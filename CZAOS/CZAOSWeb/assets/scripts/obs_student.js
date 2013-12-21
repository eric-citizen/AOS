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
                totalTime = (new Date(data.ObserveEnd) - new Date(data.ObserveStart)) / 60000;
                console.log(data);
                alert('Welcome Student');
                var sn = $("#studentNumber");
                for (var i = 0; i != parseInt(data.ObserverNo); i++) {
                    $(sn).append("<option val='" + (i + 1) + "'>Student #" + (i + 1) + "</option>");
                }
                exhibitName = data.Exhibit;
                $('#step2').fadeIn(0);
                $('#step1').fadeOut(0);
                window.AOS.czaos_get('animalObservation', {}, '#observationAnimals', false, { observationId: observationID });
                window.AOS.czaos_get_student_filtered_list('behaviorCategory', {}, '#behaviorControl', true, { exhibitId: exhibitID });
                window.AOS.czaos_get_locations('exhibitlocation', {}, '#zoneControl', true, { exhibitId: exhibitID });
                window.AOS.czaos_get_no_slide('crowd/', {noActiveImage: true, touch: false}, '#crowdControl', true);
                window.AOS.czaos_get_no_slide('weatherCondition/', {touch: false}, '#weatherControl', true);
                window.AOS.czaos_get_no_slide('wind/', { noActiveImage: true, touch: false }, '#windControl', true);

            } else {
                toastr.error('Sorry, wrong password. Please try again.', 'Password Incorrect');
                $('#studentLogin').removeAttr('disabled');
                $('#studentLogin').val('Submit');
            }
        }).fail(function() {
            $('#studentLogin').removeAttr('disabled');
            $('#studentLogin').val('Submit');
        });
    };

    window.AOS.login().done(function() {
        ////preload weather screen icons
        //window.AOS.preloadIcons("/assets/images/icons/observation/weather", ".png");
        //window.AOS.preloadIcons("/assets/images/icons/observation/wind", ".png");
        //window.AOS.preloadIcons("/assets/images/icons/observation/crowd", ".png");
    });
    


});

var observationID=0;
var exhibitID=0;
var obsWeather;
var exhibitName;
var username = 'none';
var timerInterval;
var totalTime;
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
                $("#temperature").html(ui.value);
            }
        });
        $("#temperature").html($("#temperature-slider").slider("value"));
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

    //preload the observation screen icons
    window.AOS.preloadIcons("/assets/images/icons/observation/behaviors", ".png");
}

function startObservation() {
    $('#observation-page').addClass(exhibitName);
    $('#animal-name').html($("#observationAnimals option:selected").attr('data-name'));
    $('#common-name').html($("#observationAnimals option:selected").attr('data-common-name'));
    $('#enviromentPanel').fadeOut(0);
    $('#observationPanel').fadeIn(0);
    $(window).trigger('resize');

    //register buttons
    $("#saveRecord").click(function() {
        if (!hasTakenRecord) {
            //getRecordToBeSaved();
            toastr.success("Your observation has been saved", "Record Saved");
            hasTakenRecord = true;
        } else {
            //getRecordToBeSaved();
            toastr.success("Your observation was updated successfully", "Record Updated");
        }
        updatePreviouslySavedFields();
    });
    
    $('.pause-button').click(Timer.pause);
    
    Timer.startTime(timerInterval, totalTime,timerSaveFunction);
}

function timerSaveFunction() {
    var observation = getRecordToBeSaved();
    console.log(observation);
    observationRecords.data.records.push(observation);
    observationRecords.saveToLocal();
    toastr.success("Your observation for this interval has been saved", "Record Saved");

    //reset timebar
    hasTakenRecord = false;
}

function finishObservation() {
    Timer.pause();
    //paused = true;
    $('#observation-page').toggleClass(exhibitName);
    $('#observationPanel').fadeOut(0);
    $('#finalizePanel').fadeIn(0);
    $(window).trigger('resize');
    $('#backToObservation').click(function () {
        $('#observation-page').toggleClass(exhibitName);
        $('#observationPanel').fadeIn(0);
        $('#finalizePanel').fadeOut(0);
        Timer.pause();
        //paused = false;
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
    $('#previous-behavior').attr('data-cateogry-code', $("#behaviorControl li.selected").attr('name'));
    $('#previous-location').html($("#zoneControl li.selected").attr('data-category'));
    $('#previous-location').attr('data-value', $("#zoneControl li.selected").attr('name'));
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

function getRecordToBeSaved() {
    return new record({ ZooID: $("#observationAnimals").val(), LocationID: $('#previous-location').attr('data-value'), BvrCat: $("#previous-behavior").html(), BvrCatCode: $('#previous-behavior').attr('data-cateogry-code') });
    //return new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') });
}