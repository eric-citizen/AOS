// Local Storage wrangler... trying to make life easier.
// you give the Locus an id, to make sure its our local storage.


        var Locus = function(id){
            this.data = {};
            this.local = window.localStorage || {};

            // if the local storage has never been accessed by us... lets delete whatever might be in here.
            if(!this.local[id] && id){
                this.local.clear();
                this.data[id]=Date.now();
            }
        }

        Locus.prototype.get = function(what) {
            this.data[what]||undefined
            return this;
        };

        Locus.prototype.set = function(what,value) {
            this.data[what]=value
            return this;
        };

        Locus.prototype.clear = function(){
            this.data={};
            this.local.clear();
            return this;
        }

        Locus.prototype.loadFromLocal = function(){
            if(this.local){
                for (var i in this.local){
                    if(this.local[i].split('{').length!=1){
                        this.data[i]=JSON.parse(this.local[i]);
                    }else{
                        if(parseFloat(this.local[i])==this.local[i]){
                            this.data[i]=parseFloat(this.local[i]);
                        }else{
                            this.data[i]=this.local[i];
                        }
                    }
                }
            }
        }

        Locus.prototype.saveToLocal = function(){
            if(this.data){
                for (var i in this.data){
                    if(typeof this.data[i] == 'object'){
                        this.local[i]=JSON.stringify(this.data[i]);
                    }else{
                        this.local[i]=this.data[i];
                    }
                }
            }
        }

        var SimpleTemplate={
                ver:'1.2',
                template:{}
            };
             
            SimpleTemplate._fill = function(who,where,props){
                var temp=SimpleTemplate.template[who] || who;
                var identifier;
                for (var i in where){
                    if(typeof(where[i])=='object'){
                        for (var j in where[i]){
                            identifier = new RegExp("\\%" + i + "."+j+"\\%", "g");
                            temp=temp.replace(identifier,where[i][j]);
                        }
                    }else{
                        var sim=where[i];
                        if(typeof(sim)=='boolean' || typeof(sim)=='number') sim.toString(); // convert booleans & numbers to strings for keying
                        var simCheck=sim;
                        identifier = new RegExp("\\%" + i + "\\%", "g");
                        if(simCheck=='Yes' || simCheck=='true' || simCheck== 'false' || simCheck=='No'){
                            if(props && props[i]){
                                temp=temp.replace(identifier,props[i][simCheck]);
                            }
                        }else{
                            temp=temp.replace(identifier,where[i]);
                        }
                    }
                }
                return temp;
            }
        
            SimpleTemplate.fill = function(who, where, props){
                var temp = [];
                console.log(typeof(where))
                if(where.length && typeof(where)!='string'){
                    for (var i= 0; i!= where.length; i++){
                        temp.push(SimpleTemplate._fill(who,where[i],props));
                    }
                }else{
                    temp.push(SimpleTemplate._fill(who,where,props));
                }
                return(temp.join(''));
            }
             
            SimpleTemplate.loadTemplate = function(who){
                console.log(who);
                if(document.getElementById(who)){
                    SimpleTemplate.template[who]=document.getElementById(who).innerHTML;
                }else{
                    console.log('failed to load template ['+who+']')
                }
            }
             
            SimpleTemplate.loadTemplates = function(){
                var temp = document.getElementsByTagName('script');
                console.log(temp)
                for (var i=0;i!=temp.length;i++){
                    console.log(temp[i].type)
                    if(temp[i].type=='simple/template'){
                        var who = temp[i].id;
                        SimpleTemplate.template[who]=temp[i].innerHTML;
                    }
                }
            }





        $(function(){
            SimpleTemplate.loadTemplates();
            function setToken(token) {
                $.cookie('CZAOSCookie', token);
            }
            function getToken() {
                if ($.cookie('CZAOSCookie') == null) {
                    return "";
                }
                else {
                    return $.cookie('CZAOSCookie');
                }
            }
            function deleteToken() {
                $.removeCookie('CZAOSCookie');
                $.cookie('CZAOSCookie') == null;
            }
            deleteToken();
            function login() {
                var API_URL = "api/security/login";            
                var creds = "YXBpdXNlcjphYmNkMTIzNA==";
                $.ajax({
                    type: "GET",
                    cache: false,
                    url: API_URL,
                    beforeSend: function (request) {
                        request.setRequestHeader("Authorization", creds);
                    },      
                    processData: false,
                    statusCode: {
                        401: function (data) {                        
                           // alert("Login unsuccessful");
                        },
                        200: function (data) {
                            setToken(data);
                           // alert('login sucessful')
                        }
                    }
                })
                .done(function (result) {
                    console.log("done: " + result);   
                    //czaos_get('animal/',{},'#animalsControl');
                    //czaos_get('observation/',{},'#observationData');    
                })
                .fail(function (jqxhr, textStatus, error) {
                    var err = textStatus + ', ' + error;
                    console.log("Request Failed: " + API_URL + " Err: " + err);                

                })
                .always(function () {
                    console.log("login - always");
                });
            }

            
            

            czaos_get = function (api,params,who,strip,data2){
                var params= params || {};
                $.ajax({
                    url: 'api/'+api,
                    beforeSend: function (request) {
                        request.setRequestHeader("CZAOSToken", getToken());
                        for(var i in data2){
                            request.setRequestHeader(i, data2[i]);
                        }
                    },
                    cache: false,
                    type: 'GET',
                    data: $.toJSON(params),
                    contentType: 'application/json; charset=utf-8'
                }).done(function(data){
                    console.log(data)
                    if(who){
                        $(who).html(SimpleTemplate.fill(api.split('/')[0],data));
                        if(strip) {
                            $(who).find('li').click(function(){
                                $(who).find('li').removeClass('selected');
                                $(who).find('img').each(function(){
                                    $(this).attr('src',$(this).attr('src').split('-active').join(''));
                                });
                                $(this).addClass('selected');
                                $(this).find('img').attr('src',$(this).find('img').attr('src').split('.pn').join('-active.pn'));
                            })
                            $(who).slideStrip({slide:true});
                        }
                    }
                });
               
            }

            czaos_post = function (api,params,data2) {
                params = params || {};
                $.ajax({
                    url: 'api/' + api,
                    beforeSend: function (request) {
                        request.setRequestHeader("CZAOSToken", getToken());
                        for (var i in data2) {
                            request.setRequestHeader(i, data2[i]);
                        }
                    },
                    cache: false,
                    type: 'POST',
                    data: $.toJSON(params),
                    contentType: 'application/json; charset=utf-8'
                });
<<<<<<< HEAD
                $("#saveRecord").click(function(){
                    if(cantakerecord){
                        observationRecords.data.records.push(new record({AnimalID:$("#observationAnimals").val(), LocationID:$("#zoneControl li.selected").attr('name'), BvrCatCode:$("#behaviorControl li.selected").attr('name')}));
                        console.log(observationRecords.data.records);
                        observationRecords.saveToLocal();
                    }
                    cantakerecord=false;
                })
=======
>>>>>>> origin/AdamBranch
            }

            checkLogin = function(api,pass){
                var params= params || {};
                $.ajax({
                    url: 'api/'+api,
                    beforeSend: function (request) {
                        request.setRequestHeader("CZAOSToken", getToken());
                    },
                    cache: false,
                    type: 'GET',
                    data: $.toJSON(params),
                    contentType: 'application/json; charset=utf-8'
                }).done(function(data){
                    if(data.StudentPass==pass){
                        console.log(data);
                        alert('Welcome Student');
                        var sn=$("#studentNumber");
                        for(var i = 0 ; i != parseInt(data.ObserverNo);i++){
                            $(sn).append("<option val='"+(i+1)+"'>Student #"+(i+1)+"</option>");
                        }
                        $('body').addClass(data.Exhibit);
                        $('#step2').fadeIn(0);
                        $('#step1').fadeOut(0);
                        czaos_get('animalObservation',{},'#observationAnimals',false,{observationId:observationID});
                        czaos_get('behaviorCategory',{},'#behaviorControl',true,{exhibitId:exhibitID});
                        czaos_get('location',{},'#zoneControl',false,{exhibitId:exhibitID});
                        czaos_get('crowd/',{},'#crowdControl',true);
                        czaos_get('weatherCondition/',{},'#weatherControl',true);

                    }else{
                        alert('Sorry wrong password. try again.')
                    }
                });
            }

        login();

        });
        var observationID=0;
        var exhibitID=0;
        var obsWeather;
        var username='none';
        var observationRecords = new Locus("observationRecords");
        observationRecords.clear();
        observationRecords.data.records=[];
        var cantakerecord=true;
        function loginStudent(){
            var id=observationID= $("#obsID").val();
            var pass= $("#pass").val();
            checkLogin('observation/'+id,pass);
            
        }
        function gotoWeather(){
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
        function startObservation(){
             $('#enviromentPanel').fadeOut(0);
             $('#observationPanel').fadeIn(0);
             $(window).trigger('resize');

             $("#saveRecord").click(function () {
                 if (cantakerecord) {
                     observationRecords.data.records.push(new record({ ZooID: $("#observationAnimals").val() , LocationID: $("#zoneControl").val(), BvrCatCode: $("#behaviorControl li.selected").attr('name') }));
                     console.log(observationRecords.data.records);
                     observationRecords.saveToLocal();
                 }
                 cantakerecord = false;
             });

             startTime(10);
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
        var me={long:0,lat:0}

        function showPosition(point){
            me={
              lat:point.coords.latitude,
              long:point.coords.longitude
            };
            getWeatherHere();
        }
        var record= function(ref){
            this.ObservationID  = observationID;
            this.Username       = username;
            this.AnimalID       = ref.AnimalID;
            this.ZooID          = ref.ZooID;
            this.BvrCat         = ref.BvrCat;
            this.BvrCatCode     = ref.BvrCatCode;
            this.Behavior       = ref.Behavior;
            this.BehaviorCode   = ref.BehaviorCode;
            this.LocationID     = ref.LocationID;
            this.ObserverTime    = new Date();
            this.Deleted        = 0;
            this.Flagged        = 0;
        }
        var weather= function(ref){
            this.ObservationID  = observationID;
            this.Username       = username;
            this.weatherID      = ref.weatherID;
            this.Temperature    = ref.Temperature;
            this.WindID         = ref.WindID;
            this.CrowdID        = ref.CrowdID;
            this.WeatherTime    = new Date();;
            this.Deleted        = 0;
            this.Flagged        = 0;
        }
        var watch=navigator.geolocation.getCurrentPosition(showPosition);
