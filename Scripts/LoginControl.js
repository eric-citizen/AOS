var LoginControl = (function (message) {
    var configure = function() {
        $('#login-button').on('click', function () {
            var username = $('#username').value();
            var password = $('#password').value();
            LoginControl.Login(username, password);
        });
    };

    var login = function(username, password) {
        var API_URL = "api/security/login";            
        var creds = "YXBpdXNlcjphYmNkMTIzNA=="; //apiuser:abcd1234

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
                    showError("Login unsuccessful");
                },
                200: function(data)
                {
                    setToken(data);
                    console.log("token: " + data);
                    showMessage("Logged In. Token is " + data);
                    writeWelcomeMessage();
                }
            }
        })
        .done(function (result) {
            console.log("done: " + result);                
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ', ' + error;
            console.log("Request Failed: " + API_URL + " Err: " + err);                

        })
        .always(function () {
            console.log("login - always");
        });
    };

    var logout = function() {

    };
    
    return {
        Configure: configure,
        Login: login,
        Logout: logout
    };
})("Hello World");