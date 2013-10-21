﻿$(function () {
    SimpleTemplate.loadTemplates();
    window.AOS.deleteToken();

    checkLogin = function (api, login, pass) {
        $.ajax({
            url: 'api/' + api,
            beforeSend: function(request) {
                request.setRequestHeader('CZAOSToken', window.AOS.getToken());
            },
            cache: false,
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        }).done(function(data) {
            if (data.TeacherPass == pass && data.TeacherLogin == login) {
                exhibitID = data.ExhibitID;
                console.log(data);
                alert('Welcome Teacher');
                $('body').addClass(data.Exhibit);
                populateObservationInfo(data);
                gotoObservationDetails();
            } else {
                alert('Sorry wrong password and/or login. Try again.');
            }
        });
    };

    window.AOS.login();
});

var observationID = 0;
var exhibitID = 0;

function loginTeacher() {
    var id = observationID = $("#obsID").val();
    var login = $("#login").val();
    var pass = $("#pass").val();
    checkLogin('observation/' + id, login, pass);
}

function gotoObservationDetails() {
    $('#step1').fadeOut(0);
    $('#observationInfo').fadeIn(0);

    //function for verifying the data?
}

function populateObservationInfo(observation) {
    //make get requests to get appropriate data
    window.AOS.get('exhibit/' + observation.ExhibitID, {}).done(function(data) {
        //display AnimalRegion or RegionName?
        $('#region').html(data.RegionName);
        $('#exhibit').html(data.Exhibit);
    });
    window.AOS.get('school/' + observation.SchoolID, {}).done(function(data) {
        //School and DistrictName\
        $('#district').html(data.DistrictName);
        $('#schoolName').html(data.School);
        console.log(data);
    });
    window.AOS.get('animalobservation', { observationId: observation.ObservationID }).done(function (data) {
        //display all of the animals CommmonName or house name?
        for (var i in data) {
            $('#animal-list').append('<div>' + data[i].CommonName + '</div>');
        }
        console.log(data);
    });
    
    //set fields from observation object
    var date = new Date;
    $('#date').html(date.getDay() + '-' + (date.getMonth()+1) + '-' + date.getFullYear());
    $('#observationId').html(observation.ObservationID);
    var start = new Date(observation.ObserveStart),
        end = new Date(observation.ObserveEnd);
    $('#time').html(start.time() + ' - ' + end.time());
    $('#interval').html(observation.Interval + ' Minute(s)');
    $('#studentPass').html(observation.StudentPass);
    $('#observerCount').html(observation.ObserverNo);
}