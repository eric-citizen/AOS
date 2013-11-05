$(function () {
    //set up toastr options
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    //initialize obs_student
    SimpleTemplate.loadTemplates();

    window.AOS.deleteToken();

    checkLogin = function(api, pass) {
        var params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function(request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8',
            statusCode: {
                404: function (data) {
                    alert('Could not find the observation.');
                }
            }
        }).done(function(data) {
            if (data.StudentPass == pass) {
                exhibitID = data.ExhibitID;
                timerInterval = data.Interval * 60;
                console.log(data);
                alert('Welcome Student');
                var sn = $("#studentNumber");
                for (var i = 0; i != parseInt(data.ObserverNo); i++) {
                    $(sn).append("<option val='" + (i + 1) + "'>Student #" + (i + 1) + "</option>");
                }
                $('body').addClass(data.Exhibit);
                $('#step2').fadeIn(0);
                $('#step1').fadeOut(0);
                window.AOS.czaos_get('animalObservation', {}, '#observationAnimals', false, { observationId: observationID });
                window.AOS.czaos_get_student_filtered_list('behaviorCategory', {}, '#behaviorControl', true, { exhibitId: exhibitID });
                window.AOS.czaos_get_student_filtered_list('exhibitlocation', {}, '#zoneControl', false, { exhibitId: exhibitID });
                window.AOS.czaos_get_no_slide('crowd/', {noActiveImage: true, touch: false}, '#crowdControl', true);
                window.AOS.czaos_get_no_slide('weatherCondition/', {touch: false}, '#weatherControl', true);
                window.AOS.czaos_get_no_slide('wind/', { noActiveImage: true, touch: false }, '#windControl', true);

            } else {
                alert('Sorry wrong password. Try again.');
            }
        }).fail(function() {
            $('#studentLogin').removeAttr('disabled');
            $('#studentLogin').val('Submit');
        });
    };

    window.AOS.login();

});

var observationID=0;
var exhibitID=0;
var obsWeather;
var username = 'none';
var paused = true;
var timerInterval = 0;
var observationRecords = new Locus("observationRecords");
observationRecords.clear();
observationRecords.data.records=[];
var hasTakenRecord = false;


function loginStudent() {
    $('#studentLogin').attr('disabled', 'disabled');
    $('#studentLogin').val('Please Wait');

    var id=observationID= $("#obsID").val();
    var pass= $("#pass").val();
    checkLogin('observation/'+id,pass);
            
}

function gotoWeather() {
        $('#step2').fadeOut(0);
        $('#enviromentData').fadeIn(0);
             
        username = $("#studentNumber").val();
    
    //set up the slider for the temperature
        $(function () {
            $("#temperature-slider").slider({
                //range: "max",
                min: 0,
                max: 120,
                value: 60,
                slide: function (event, ui) {
                    $("#temperature").val(ui.value);
                }
            });
            $("#temperature").val($("#temperature-slider").slider("value"));
        });

        $("#saveWeather").click(function () {
            obsWeather = new weather({ Temperature: $("#temperature-slider").slider("value"), WeatherConditionID: $("#weatherControl li.selected").attr('name'), CrowdID: $("#crowdControl li.selected").attr('name'), WindID: $("#windControl li.selected").attr('name')});
            console.log(obsWeather);
            
            //check to make sure weather and crowd have been selected
            if (!obsWeather.WeatherConditionID || !obsWeather.CrowdID || !obsWeather.WindID) {
                alert('Please select a value for all options.');
            } else {
                $('#saveWeather').attr('disabled', 'disabled');
                $('#saveWeather').val('Please Wait');
                
                window.AOS.czaos_post('weather', obsWeather, {}).done(function() {
                    startObservation();
                }).fail(function() {
                    toastr.error("Please try again.", "There was an error saving your weather information");
                    $('#saveWeather').removeAttr('disabled');
                    $('#saveWeather').val('Submit');
                });
            }
        });
}

function startTime(time) {
    paused = false;
    var count=0;
    var interval = setInterval(function () {
        if (!paused) {
            displayTime(count);
            $(".timebar").css('width', ((count / time) * 100) + '%');
            if (count == time) {
                //if the user did not save the record for this interval, go ahead and take whatever values are currently selected as the record
                if (!hasTakenRecord) {
                    updateRecordToBeSaved();
                }
                //save record to local storage and display message
                observationRecords.data.records.push(recordToBeSaved);
                observationRecords.saveToLocal();
                toastr.success("Your observation for this interval has been saved", "Record Saved");
                
                hasTakenRecord = false;
                clearInterval(interval);
                startTime(time);
            }
            count++;
        }
    }, 1000);
}

function displayTime(count) {
    var time = timerInterval - count;
    var minutes = Math.floor(time / 60);
    var seconds = time % 60;
    $('.time').html(minutes + ':' + ((seconds < 10) ? "0" : "") + seconds);
}

function startObservation() {
        $('#animal-name').html($("#observationAnimals option:selected").attr('data-name'));
        $('#enviromentPanel').fadeOut(0);
        $('#observationPanel').fadeIn(0);
        $(window).trigger('resize');

        $("#saveRecord").click(function() {
            if (!hasTakenRecord) {
                updateRecordToBeSaved();
                toastr.success("Your observation has been saved", "Record Saved");
                hasTakenRecord = true;
            } else {
                updateRecordToBeSaved();
                toastr.success("Your observation was updated successfully", "Record Updated");
            }
            updatePreviouslySavedFields();
        });

        startTime(timerInterval);
}

function finishObservation() {
    paused = true;
    $('#observationPanel').fadeOut(0);
    $('#finalizePanel').fadeIn(0);
    $(window).trigger('resize');
    $('#backToObservation').click(function () {
        $('#observationPanel').fadeIn(0);
        $('#finalizePanel').fadeOut(0);
        paused = false;
        $('#finalizeObservation').unbind('click');
        $('#backToObservation').unbind('click');
    });
            
    //set up listeners for finish observation button
    $('#finalizeObservation').click(function () {
        handleSave();
    });
}

function handleSave() {
    var $errorToast, $infoToast;
    //disable buttons
    //$('#finalizeObservation').attr("disabled", "disabled");
    //$('#finalizeObservation').toggleClass("disabled");
    $('#finalizeObservation').unbind("click");
    $('#backToObservation').unbind("click");
    $('#finalizeObservation').toggleClass("button");
    $('#finalizeObservation').toggleClass("disabled-button");
    $('#backToObservation').toggleClass("button");
    $('#backToObservation').toggleClass("disabled-button");

    if ($errorToast) {
        toastr.clear($errorToast);
    }
    $infoToast = toastr.info("Your observation is being saved", "Please wait");
    //attempt to send all records from local storage to the server
    observationRecords.loadFromLocal();
    var records = observationRecords.data.records;
    //if success show finished page
    window.AOS.czaos_post('observationrecord', records, {}).done(function (data) {
        toastr.clear($infoToast);
        toastr.options.timeOut = 0;
        toastr.options.onclick = function () {
            location.reload();
        };
        toastr.success("Please click to refresh page.", "Observation Records Successfully Saved");
    }).fail(function (data) {
        //$('#finalizeObservation').toggleClass('disabled');
        //$('#finalizeObservation').removeAttr('disabled');
        $('#finalizeObservation').click(handleSave);
        $('#backToObservation').click(returnToObservation);
        $('#backToObservation').toggleClass("button");
        $('#backToObservation').toggleClass("disabled-button");
        $('#finalizeObservation').toggleClass("button");
        $('#finalizeObservation').toggleClass("disabled-button");
        $errorToast = toastr.error("Please try again.", "There was an error saving your observation");
    });
}

function updatePreviouslySavedFields() {
    $('#previous-behavior').html($("#behaviorControl li.selected").attr('data-category'));
    $('#previous-location').html($("#zoneControl :selected").html());
}

var me = { long: 0, lat: 0 };

function showPosition(point){
    me={
        lat:point.coords.latitude,
        long:point.coords.longitude
    };
    //getWeatherHere();
}

var record = function(ref) {
    this.ObservationID = observationID;
    this.Username = username;
    //this.AnimalID = ref.AnimalID;
    this.ZooID = ref.ZooID;
    this.BvrCat = ref.BvrCat;
    this.BvrCatCode = ref.BvrCatCode;
    this.Behavior = "";
    this.BehaviorCode = "";
    this.LocationID = ref.LocationID;
    this.ObserverTime = new Date();
    this.Deleted = 0;
    this.Flagged = 0;
};
var weather = function(ref) {
    this.ObservationID = observationID;
    this.Username = username;
    this.WeatherConditionID = ref.WeatherConditionID;
    this.Temperature = ref.Temperature;
    this.WindID = ref.WindID;
    this.CrowdID = ref.CrowdID;
    this.WeatherTime = new Date();
    this.Deleted = 0;
    this.Flagged = 0;
};
var watch = navigator.geolocation.getCurrentPosition(showPosition);

var recordToBeSaved;

function updateRecordToBeSaved() {
    recordToBeSaved = new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') });
    console.log(recordToBeSaved);
}