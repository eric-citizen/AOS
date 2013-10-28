$(function () {
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

    checkObservationLogin = function(api, params) {
        params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function(request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in params) {
                    request.setRequestHeader(i, params[i]);
                }
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8',
            statusCode: {
                404: function(data) {
                    alert('Could not find the observation.');
                }
            }
        }).done(function(data) {
            //check if the user is set up for this observation
            if (userCanAccessObservation(data)) {
                //if yes make all api calls
                window.AOS.get('observation/' + observationID, {}).done(function(observation) {

                    populateObservationInfo(observation);
                    gotoObservationDetails();

                    getObservationData();

                    //transition to next step
                    $('body').addClass(observation.Exhibit);
                    $('#step2').fadeIn(0);
                    $('#step1').fadeOut(0);

                    exhibitID = observation.ExhibitID;
                    timerInterval = observation.Interval * 60;
                    console.log(observation);
                });
            }
        });
    };

    login = function(username, password) {
        var API_URL = "api/security/login";
        var creds = Base64.encode(username + ':' + password);
        $.ajax({
            type: "GET",
            cache: false,
            url: API_URL,
            beforeSend: function(request) {
                request.setRequestHeader("Authorization", creds);
            },
            processData: false,
            statusCode: {
                401: function(data) {
                    alert("Login unsuccessful");
                },
                200: function(data) {
                    window.AOS.setToken(data);
                    $('#step1').fadeOut(0);
                    $('#step2').fadeIn(0);

                    username = $("#username").val();
                }
            }
        }).done(function(result) {
            console.log("done: " + result);
        }).fail(function(jqxhr, textStatus, error) {
            var err = textStatus + ', ' + error;
            console.log("Request Failed: " + API_URL + " Err: " + err);
        }).always(function() {
            console.log("login - always");
        });
    };

});

var observationID = 0;
var exhibitID = 0;
var obsWeather;
var username = 'none';
var paused = true;
var timerInterval = 0;
var observationRecords = new Locus("observationRecords");
observationRecords.clear();
observationRecords.data.records = [];
var hasTakenRecord = true;

function loginProfessional() {
    var username = $('#username').val();
    var password = $('#pass').val();
    login(username, password);
}

function loginToObservation() {
    observationID = $("#obsID").val();
    checkObservationLogin('observer/', { observationId: observationID });
}

function gotoObservationDetails() {
    $('#step2').fadeOut(0);
    $('#observationInfo').fadeIn(0);
}

function populateObservationInfo(observation) {
    //make get requests to get appropriate data
    window.AOS.get('animalobservation', { observationId: observationID }).done(function(data) {
        //display all of the animals CommmonName or house name?
        for (var i in data) {
            $('#animal-list').append('<div>' + data[i].CommonName + '</div>');
        }
        console.log(data);
    });

    //set fields from observation object
    window.AOS.get('exhibit/' + observation.ExhibitID, {}).done(function(data) {
        //display AnimalRegion or RegionName?
        $('#region').html(data.RegionName);
        $('#exhibit').html(data.ExhibitName);
    });

    $('body').addClass(observation.Exhibit);
    var date = new Date;
    $('#date').html(date.getDay() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear());
    $('#observationId').html(observation.ObservationID);
    var start = new Date(observation.ObserveStart),
        end = new Date(observation.ObserveEnd);
    $('#time').html(start.time() + ' - ' + end.time());
    $('#interval').html(observation.Interval + ' Minute(s)');
    $('#observerCount').html(observation.ObserverNo);

}

function getObservationData() {
    window.AOS.czaos_get('animalObservation', {}, '#observationAnimals', false, { observationId: observationID });
    window.AOS.czaos_get('behaviorCategory', {}, '#behaviorControl', true, { exhibitId: exhibitID });
    window.AOS.czaos_get('exhibitlocation', {}, '#zoneControl', false, { exhibitId: exhibitID });
    window.AOS.czaos_get('crowd/', {}, '#crowdControl', true);
    window.AOS.czaos_get('weatherCondition/', {}, '#weatherControl', true);
}

function gotoWeather() {
    $('#step2').fadeOut(0);
    $('#observationInfo').fadeOut(0);
    $('#enviromentData').fadeIn(0);

    $("#saveWeather").click(function() {
        obsWeather = new weather({ Temperature: $(".temp").text(), weatherID: $("#weatherControl li.selected").attr('name'), CrowdID: $("#crowdControl li.selected").attr('name') });
        console.log(obsWeather);
        startObservation();
    });
}

function startTime(time) {
    paused = false;
    var count = 0;
    var interval = setInterval(function() {
        if (!paused) {
            displayTime(count);
            $(".timebar").css('width', ((count / time) * 100) + '%');
            if (count == time) {
                if (hasTakenRecord) {
                    _.each(recordsToBeSaved, function(record) {
                        observationRecords.data.records.push(record);
                    });
                    observationRecords.saveToLocal();
                }
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
    $('.time').html(minutes + ':'+ ((seconds < 10) ? "0" : "") + seconds );
}

function startObservation() {
    //check to make sure weather and crowd have been selected
    if (!obsWeather.weatherID || !obsWeather.CrowdID) {
        alert('Please select an option for both weather and crowd');
    } else {
        window.AOS.AnimalControl().Configure(observationID);
        window.AOS.LocationControl().Configure(exhibitID);
        window.AOS.BehaviorCategoryControl().Configure(exhibitID).done(function () {
            //show appropriate objects and start the timer
            $('#enviromentPanel').fadeOut(0);
            $('#observationPanel').fadeIn(0);
            $('#button-container').fadeIn(0);
            startTime(timerInterval);
        });
        //window.AOS.BehaviorControl().Configure(exhibitID);

        $(window).trigger('resize');

        $("#saveRecord").click(function () {
            var aos = window.AOS;
            updateRecordsToBeSaved({
                LocationID: aos.LocationControl().SelectedLocation().LocationID(),
                BvrCat: aos.BehaviorCategoryControl().SelectedCategory().BvrCat(),
                BvrCatCode: aos.BehaviorCategoryControl().SelectedCategory().BvrCatCode(),
                Behavior: aos.BehaviorControl().SelectedBehavior().Behavior(),
                BehaviorCode: aos.BehaviorControl().SelectedBehavior().BehaviorCode()
            });
            if (!hasTakenRecord) {
                toastr.success("Your observation has been saved", "Record Saved");
                hasTakenRecord = true;
            } else {
                toastr.success("Your observation was updated successfully", "Record Updated");
            }
        });

        //startTime(10);
    }
}

function updateRecordsToBeSaved(params) {
    //clear current items in array
    recordsToBeSaved = [];
    //add record to recordsToBeSaved foreach animal selected
    _.each(window.AOS.AnimalControl().SelectedAnimals(), function (animal) {
        recordsToBeSaved.push(
        new record(
            {
                ZooID: animal.ZooID,
                LocationID: params.LocationID,
                BvrCat: params.BvrCat,
                BvrCatCode: params.BvrCatCode,
                Behavior: params.Behavior,
                BehaviorCode: params.BehaviorCode
            }));
    });
    console.log(recordsToBeSaved);
}

function finishObservation() {
    paused = true;
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
    paused = false;
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

function getWeatherHere() {
    var loc = "//api.forecast.io/forecast/6edffeaac741bd27f996522ead02a772/" + me.lat + ',' + me.long;
    $.getJSON(loc + "?callback=?", function(data) {
        data.currently.temperature = Math.round(data.currently.temperature);
        data.currently.windSpeed = Math.round(data.currently.windSpeed);
        $('#weatherAPIoutput').html(SimpleTemplate.fill('weatherAPI', data.currently));
    });
}

function userCanAccessObservation(observerRecords) {
    //iterate through each record and return true if there is a record
    //where record.Username = username
    $(observerRecords).filter(function() {
        return this.Username = username;
    });
    if (observerRecords.length <= 0) {
        alert('You are not authorized to access this observation.');
        return false;
    }
        //also filter on whether the record is Locked or Deleted and return appropriate message
    else if (observerRecords[0].Locked) {
        alert('The observation is locked.');
        return false;
    } else if (observerRecords[0].Deleted) {
        alert('The observation has been marked as deleted.');
        return false;
    } else {
        return true;
    }
}

var me = { long: 0, lat: 0 };

function showPosition(point) {
    me = {
        lat: point.coords.latitude,
        long: point.coords.longitude
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
    this.ObserverTime = new Date().toUTCString();
    this.Deleted = 0;
    this.Flagged = 0;
};
var recordsToBeSaved = [];

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