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
            contentType: 'application/json; charset=utf-8'
        }).done(function(data) {
            if (data.StudentPass == pass) {
                exhibitID = data.ExhibitID;
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
                window.AOS.czaos_get('crowd/', {}, '#crowdControl', true);
                window.AOS.czaos_get('weatherCondition/', {}, '#weatherControl', true);

            } else {
                alert('Sorry wrong password. Try again.');
            }
        });
    };

    window.AOS.login();

});

var observationID=0;
var exhibitID=0;
var obsWeather;
var username = 'none';
var paused = true;
var observationRecords = new Locus("observationRecords");
observationRecords.clear();
observationRecords.data.records=[];
var hasTakenRecord = false;


function loginStudent(){
    var id=observationID= $("#obsID").val();
    var pass= $("#pass").val();
    checkLogin('observation/'+id,pass);
            
}

function gotoWeather() {
        $('#step2').fadeOut(0);
        $('#enviromentData').fadeIn(0);
             
        username = $("#studentNumber").val();

        $("#saveWeather").click(function () {
            obsWeather = new weather({ Temperature: $(".temp").text(), weatherID: $("#weatherControl li.selected").attr('name'), CrowdID: $("#crowdControl li.selected").attr('name') });
            console.log(obsWeather);
            startObservation();
        });
}

function startTime(time) {
    paused = false;
    var count=0;
    var interval = setInterval(function () {
        if (!paused) {
            $(".timebar").css('width', ((count / time) * 100) + '%');
            count++;
            if (count == time) {
                if (hasTakenRecord) {
                    observationRecords.data.records.push(recordToBeSaved);
                    observationRecords.saveToLocal();
                }
                hasTakenRecord = false;
                clearInterval(interval);
                startTime(time);
            }
        }
    }, 1000);
}
function startObservation() {
    //check to make sure weather and crowd have been selected
    if (!obsWeather.weatherID || !obsWeather.CrowdID) {
        alert('Please select an option for both weather and crowd');
    } else {
        $('#enviromentPanel').fadeOut(0);
        $('#observationPanel').fadeIn(0);
        $(window).trigger('resize');

        $("#saveRecord").click(function() {
            if (!hasTakenRecord) {
                recordToBeSaved = new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') });
                //observationRecords.data.records.push(new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') }));
                console.log(recordToBeSaved);
                toastr.success("Your observation has been saved", "Record Saved");
                hasTakenRecord = true;
            } else {
                recordToBeSaved = new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') });
                toastr.success("Your observation was updated successfully", "Record Updated");
            }
        });

        startTime(10);
    }
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
    $('#finalizeObservation').attr("disabled", "disabled");
    $('#finalizeObservation').toggleClass("disabled");

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
        $('#finalizeObservation').toggleClass('disabled');
        $('#finalizeObservation').removeAttr('disabled');
        $errorToast = toastr.error("Please try again.", "There was an error saving your observation");
    });
}

function getWeatherHere(){
    var loc="//api.forecast.io/forecast/6edffeaac741bd27f996522ead02a772/"+me.lat+','+me.long;
    $.getJSON(loc + "?callback=?", function(data) {
        data.currently.temperature=Math.round(data.currently.temperature);
        data.currently.windSpeed=Math.round(data.currently.windSpeed);
        $('#weatherAPIoutput').html(SimpleTemplate.fill('weatherAPI',data.currently));
    });
};
var me = { long: 0, lat: 0 };

function showPosition(point){
    me={
        lat:point.coords.latitude,
        long:point.coords.longitude
    };
    getWeatherHere();
}

var record = function(ref) {
    this.ObservationID = observationID;
    this.Username = username;
    this.AnimalID = ref.AnimalID;
    this.ZooID = ref.ZooID;
    this.BvrCat = ref.BvrCat;
    this.BvrCatCode = ref.BvrCatCode;
    this.Behavior = ref.Behavior;
    this.BehaviorCode = ref.BehaviorCode;
    this.LocationID = ref.LocationID;
    this.ObserverTime = new Date();
    this.Deleted = 0;
    this.Flagged = 0;
};
var weather = function(ref) {
    this.ObservationID = observationID;
    this.Username = username;
    this.weatherID = ref.weatherID;
    this.Temperature = ref.Temperature;
    this.WindID = ref.WindID;
    this.CrowdID = ref.CrowdID;
    this.WeatherTime = new Date();
    ;
    this.Deleted = 0;
    this.Flagged = 0;
};
var watch = navigator.geolocation.getCurrentPosition(showPosition);

var recordToBeSaved;
