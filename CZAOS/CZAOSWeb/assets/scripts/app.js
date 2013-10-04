window.App = window.App || {};
window.App.baseURL = "http://localhost:53637";
//window.App.baseURL = "http://animalobservationsystem.finecitizensdev.com";

window.App.ApiHelper = (function() {
    var getToken = function() {
        if ($.cookie('CZAOSCookie') == null) {
            return "";
        }
        else {
            return $.cookie('CZAOSCookie');
        }
    };

    var getAll = function(apiURL) {
        return $.ajax({
            url: window.App.baseURL + apiURL,
            beforeSend: function(request) {
                request.setRequestHeader("CZAOSToken", getToken());
            },
            cache: false,
            type: 'GET',
            contentType: 'application/json; charaset=urt-8'
        });
    };

    var get = function(apiURL, id) {

    };

    var post = function(apiURL, id) {

    };

    var put = function(apiURL, id) {

    };

    var remove = function(apiURL, id) {

    };

    return {
        GetToken: getToken,
        GetAll: getAll,
        Get: get,
        Post: post,
        Put: put,
        Remove: remove
    };
})();

window.App.DialogControl = (function() {
    var open = function(selector, target, content) {
        $(selector).dialog({
            open: function (event, ui) {
                return $(target).load(content);
            }
        });
    };

    var close = function() {

    };

    return {
        Open: open,
        Close: close
    };
})();