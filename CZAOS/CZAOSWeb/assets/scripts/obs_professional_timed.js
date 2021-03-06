﻿$(function () {
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

    observationID = $.url.param('observationId');

    window.AOS.get('observation/' + observationID, {}).done(function (observation) {
        $('body').addClass(observation.Exhibit);

        getObservationData();
        
        gotoWeather();

        exhibitID = observation.ExhibitID;
        timerInterval = observation.Interval * 60;
        totalTime = (new Date(observation.ObserveEnd) - new Date(observation.ObserveStart)) / 60000;
        console.log(observation);
    });
});

var observationID = 0;
var exhibitID = 0;
var obsWeather;
var username = 'none';
var paused = true;
var timerInterval = 0;
var totalTime = 0;
var observationRecords = new Locus("observationRecords");
observationRecords.clear();
observationRecords.data.records = [];
var hasTakenRecord = true;

function getObservationData() {
    window.AOS.czaos_get('animalObservation', {}, '#observationAnimals', false, { observationId: observationID });
    window.AOS.czaos_get('behaviorCategory', {}, '#behaviorControl', true, { exhibitId: exhibitID });
    window.AOS.czaos_get('exhibitlocation', {}, '#zoneControl', false, { exhibitId: exhibitID });
    window.AOS.czaos_get('crowd/', {noActiveImage: true}, '#crowdControl', true);
    window.AOS.czaos_get('weatherCondition/', {}, '#weatherControl', true);
}

function gotoWeather() {
    $('#step2').fadeOut(0);
    $('#observationInfo').fadeOut(0);
    $('#enviromentData').fadeIn(0);

    $("#saveWeather").click(function () {
        var wind = $('.windspeed').attr('data-wind');
        var windID = 0;
        if (wind <= 7) {
            windID = 1;
        } else if (windID <= 18) {
            windID = 2;
        } else if (windID <= 38) {
            windID = 3;
        } else {
            windID = 4;
        }
        obsWeather = new weather({ Temperature: $(".temp").text(), WeatherConditionID: $("#weatherControl li.selected").attr('name'), WindID: windID, CrowdID: $("#crowdControl li.selected").attr('name') });
        console.log(obsWeather);

        //check to make sure weather and crowd have been selected
        if (!obsWeather.WeatherConditionID || !obsWeather.CrowdID || !obsWeather.WindID) {
            alert('Please select a value for all options.');
        } else {
            $('#saveWeather').attr('disabled', 'disabled');
            $('#saveWeather').val('Please Wait');

            window.AOS.czaos_post('weather', obsWeather, {}).done(function () {
                startObservation();
            }).fail(function () {
                toastr.error("Please try again.", "There was an error saving your weather information");
                $('#saveWeather').removeAttr('disabled');
                $('#saveWeather').val('Submit');
            });
        }
    });
}

function startObservation() {
    window.AOS.AnimalControl().Configure(observationID);
    //todo make Animalcontrol return a deferred and put code inside of done function

    //show appropriate objects and start the timer
    $('#enviromentPanel').fadeOut(0);
    $('#observationPanel').fadeIn(0);
    $('#button-container').fadeIn(0);
    Timer.startTime(timerInterval, totalTime,saveFunction);

    $(window).trigger('resize');

    //register buttons
    $("#saveRecord").click(function () {
        //updateRecordsToBeSaved();

        if (!hasTakenRecord) {
            toastr.success("Your observation has been saved", "Record Saved");
            hasTakenRecord = true;
        } else {
            toastr.success("Your observation was updated successfully", "Record Updated");
        }
    });

    $('.pause-button').click(Timer.pause);
}

function saveFunction() {
    //todo: save a record for each animal group 
    updateRecordsToBeSaved();

    _.each(recordsToBeSaved, function (record) {
        observationRecords.data.records.push(record);
    });
    observationRecords.saveToLocal();
    toastr.success("Your observation for this interval has been saved", "Record Saved");

    hasTakenRecord = false;
}

function updateRecordsToBeSaved() {
    //clear current items in array
    recordsToBeSaved = [];
    //add record to recordsToBeSaved foreach animal selected
    _.each(window.AOS.AnimalControl().AnimalGroups(), function (group) {
        _.each(group.Animals, function(animal) {
            recordsToBeSaved.push(
            new record(
                {
                    ZooID: animal.ZooID,
                    LocationID: group.LocationControl().SelectedLocation().LocationID(),
                    BvrCat: group.BehaviorCategoryControl().SelectedCategory().BvrCat(),
                    BvrCatCode: group.BehaviorCategoryControl().SelectedCategory().BvrCatCode(),
                    Behavior: group.BehaviorControl().SelectedBehavior().Behavior(),
                    BehaviorCode: group.BehaviorControl().SelectedBehavior().BehaviorCode()
                }));
        });
        
    });
    console.log(recordsToBeSaved);
}

function finishObservation() {
    //paused = true;
    Timer.pause();
    $('#observationPanel').fadeOut(0);
    $('#finalizePanel').fadeIn(0);
    $(window).trigger('resize');
    $('#backToObservation').click(returnToObservation);

    //set up listeners for finish observation button
    $('#finalizeObservation').click(function () {
        handleSave();
    });
}

function returnToObservation() {
    $('#observationPanel').fadeIn(0);
    $('#finalizePanel').fadeOut(0);
    //paused = false;
    Timer.pause();
    $('#finalizeObservation').unbind('click');
    $('#backToObservation').unbind('click');
}

function handleSave() {
    var $errorToast, $infoToast;
    //disable buttons
    //$('#finalizeObservation').attr("disabled", "disabled");
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
            window.location = '/admin/observation/default.aspx';
        };
        toastr.success("Please click to return to your observations.", "Observation Records Successfully Saved");
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

function getWeatherHere() {
    var loc = "//api.forecast.io/forecast/6edffeaac741bd27f996522ead02a772/" + me.lat + ',' + me.long;
    $.getJSON(loc + "?callback=?", function (data) {
        data.currently.temperature = Math.round(data.currently.temperature);
        data.currently.windSpeed = Math.round(data.currently.windSpeed);
        $('#weatherAPIoutput').html(SimpleTemplate.fill('weatherAPI', data.currently));
    });
}

var me = { long: 0, lat: 0 };

function showPosition(point) {
    me = {
        lat: point.coords.latitude,
        long: point.coords.longitude
    };
    getWeatherHere();
}

var record = function (ref) {
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
var recordsToBeSaved = [];

var weather = function (ref) {
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