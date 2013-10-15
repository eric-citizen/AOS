$(function () {
    //initialize obs_student
    SimpleTemplate.loadTemplates();

    deleteToken();

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
                czaos_get('animalObservation', {}, '#observationAnimals', false, { observationId: observationID });
                czaos_get('behaviorCategory', {}, '#behaviorControl', true, { exhibitId: exhibitID });
                czaos_get('exhibitlocation', {}, '#zoneControl', false, { exhibitId: exhibitID });
                czaos_get('crowd/', {}, '#crowdControl', true);
                czaos_get('weatherCondition/', {}, '#weatherControl', true);

            } else {
                alert('Sorry wrong password. try again.');
            }
        });
    };

login();

});

var observationID=0;
var exhibitID=0;
var obsWeather;
var username='none';
var observationRecords = new Locus("observationRecords");
observationRecords.clear();
observationRecords.data.records=[];
var cantakerecord = true;


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

function startTime(time){
    var count=0;
            
    var interval= setInterval(function(){
        $(".timebar").css('width',((count/time)*100)+'%');
        count++;
        if(count==time){
            cantakerecord=true;
            clearInterval(interval);
            startTime(time);
        }
    },1000);
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
            if (cantakerecord) {
                observationRecords.data.records.push(new record({ ZooID: $("#observationAnimals").val(), LocationID: $("#zoneControl").val(), BvrCat: $("#behaviorControl li.selected").attr('data-category'), BvrCatCode: $("#behaviorControl li.selected").attr('name') }));
                console.log(observationRecords.data.records);
                observationRecords.saveToLocal();
            }
            cantakerecord = false;
        });

        startTime(10);
    }
}

function finishObservation() {
    $('#observationPanel').fadeOut(0);
    $('#finalizePanel').fadeIn(0);
    $(window).trigger('resize');
            
    //set up listeners for finish observation button
    $('#finalizeObservation').click(function () {
        //attempt to send all records from local storage to the server
        observationRecords.loadFromLocal();
        var records = observationRecords.data.records;
        //if success show finished page
        czaos_post('observationrecord', records, {});
        //if failure attempt to resend
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
var watch=navigator.geolocation.getCurrentPosition(showPosition);
