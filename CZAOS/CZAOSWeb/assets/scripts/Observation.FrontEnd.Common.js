﻿window.AOS = window.AOS || {};

$(function () {
    
    Date.prototype.time = function () {
        return ((this.getHours() < 10) ? "0" : "") + this.getHours() + ":" + ((this.getMinutes() < 10) ? "0" : "") + this.getMinutes();
    };

    window.AOS.setToken = function(token) {
        $.cookie('CZAOSCookie', token);
    };

    window.AOS.getToken = function () {
        if ($.cookie('CZAOSCookie') == null) {
            return "";
        } else {
            return $.cookie('CZAOSCookie');
        }
    };

    window.AOS.deleteToken = function() {
        $.removeCookie('CZAOSCookie');
        $.cookie('CZAOSCookie') == null;
    };
    
    window.AOS.czaos_get_no_slide = function (api, params, who, strip, data2) {
        var params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function (request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in data2) {
                    request.setRequestHeader(i, data2[i]);
                }
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            console.log(data);
            if (who) {
                $(who).html(SimpleTemplate.fill(api.split('/')[0], data));
                if (strip) {
                    setupSlideStrip(who, params);
                }
            }
        });
    };
    
    function setupSlideStrip(who, stripParams) {
        $(who).find('li').click(function () {
            $(who).find('li').removeClass('selected');
            $(who).find('img').each(function () {
                $(this).attr('src', $(this).attr('src').split('-active').join(''));
            });
            $(this).addClass('selected');
            if (!stripParams.noActiveImage) {
                $(this).find('img').attr('src', $(this).find('img').attr('src').split('.pn').join('-active.pn'));
            }
        });
        $(who).slideStrip({ slide: true, touch: stripParams.touch });
    }

    window.AOS.czaos_get = function(api, params, who, strip, data2) {
        var params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function(request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in data2) {
                    request.setRequestHeader(i, data2[i]);
                }
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8'
        }).done(function(data) {
            console.log(data);
            if (who) {
                $(who).html(SimpleTemplate.fill(api.split('/')[0], data));
                if (strip) {
                    $(who).find('li').click(function() {
                        $(who).find('li').removeClass('selected');
                        $(who).find('img').each(function() {
                            $(this).attr('src', $(this).attr('src').split('-active').join(''));
                        });
                        $(this).addClass('selected');
                        if (!params.noActiveImage) {
                            $(this).find('img').attr('src', $(this).find('img').attr('src').split('.pn').join('-active.pn'));
                        }
                    });
                    $(who).slideStrip({ slide: true });
                }
            }
        });
    };
    
    window.AOS.czaos_get_student_filtered_list = function (api, params, who, strip, data2) {
        var params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function (request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in data2) {
                    request.setRequestHeader(i, data2[i]);
                }
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            data = _.filter(data, function (category) {
                return !category.MaskAma;
            });
            console.log(data);
            if (who) {
                $(who).html(SimpleTemplate.fill(api.split('/')[0], data));
                if (strip) {
                    $(who).find('li').click(function() {
                        $(who).find('li').removeClass('selected');
                        $(who).find('img').each(function() {
                            $(this).attr('src', $(this).attr('src').split('-active').join(''));
                        });
                        $(this).addClass('selected');
                        $(this).find('img').attr('src', $(this).find('img').attr('src').split('.pn').join('-active.pn'));
                    });
                    $(who).slideStrip({ slide: true });
                }
            }
        });

    };
    
    window.AOS.czaos_get_locations = function (api, params, who, strip, data2) {
        var params = params || {};
        $.ajax({
            url: 'api/' + api,
            beforeSend: function (request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in data2) {
                    request.setRequestHeader(i, data2[i]);
                }
            },
            cache: false,
            type: 'GET',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            data = _.filter(data, function (category) {
                return !category.MaskAma;
            });
            console.log(data);
            if (who) {
                $(who).html(SimpleTemplate.fill(api.split('/')[0], data));
                if (strip) {
                    $(who).find('li').click(function () {
                        $(who).find('li').removeClass('selected');
                        $(this).addClass('selected');
                    });
                    $(who).slideStrip({ slide: true });
                }
            }
        });

    };

    window.AOS.czaos_post = function (api, params, data2) {
        params = params || {};
        return $.ajax({
            url: 'api/' + api,
            beforeSend: function (request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in data2) {
                    request.setRequestHeader(i, data2[i]);
                }
            },
            cache: false,
            type: 'POST',
            data: $.toJSON(params),
            contentType: 'application/json; charset=utf-8'
        });
    };
    
    window.AOS.get = function (api, params) {
        return $.ajax({
            url: 'api/' + api,
            beforeSend: function (request) {
                request.setRequestHeader("CZAOSToken", window.AOS.getToken());
                for (var i in params) {
                    request.setRequestHeader(i, params[i]);
                }
            },
            cache: false,
            type: 'GET',
            contentType: 'application/json; charaset=urt-8',
            statusCode: {
                404: function (data) {
                    alert(data.statusText);
                }
            }
        });
    };

    window.AOS.login = function() {
        var API_URL = "api/security/login";
        var creds = "YXBpdXNlcjphYmNkMTIzNA==";
        return $.ajax({
            type: "GET",
            cache: false,
            url: API_URL,
            beforeSend: function(request) {
                request.setRequestHeader("Authorization", creds);
            },
            processData: false,
            statusCode: {
                401: function(data) {
                    // alert("Login unsuccessful");
                },
                200: function(data) {
                    window.AOS.setToken(data);
                    // alert('login sucessful')
                }
            }
        })
            .done(function(result) {
                console.log("done: " + result);
                //czaos_get('animal/',{},'#animalsControl');
                //czaos_get('observation/',{},'#observationData');    
            })
            .fail(function(jqxhr, textStatus, error) {
                var err = textStatus + ', ' + error;
                console.log("Request Failed: " + API_URL + " Err: " + err);

            })
            .always(function() {
                console.log("login - always");
            });
    };

    window.AOS.preloadIcons = function (dir, fileextension) {
        $.ajax({
            //This will retrieve the contents of the folder if the folder is configured as 'browsable'
            url: dir,
            success: function (data) {
                //Lsit all png file names in the page
                $(data).find("a:contains(" + fileextension + ")").each(function () {
                    //var filename = this.href.replace(window.location.host, "").replace("http:///", "");
                    $("#preload").append($("<img src=" + this.href + "></img>"));
                });
            }
        });
    };

});