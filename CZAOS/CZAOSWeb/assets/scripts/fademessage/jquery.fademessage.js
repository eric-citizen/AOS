(function ($) {
    $.fn.center = function () {

        $(this).css("position", "absolute");
        $(this).css("top", (($(window).height() - $(this).height()) / 2) + $(window).scrollTop() + "px");
        $(this).css("left", (($(window).width() - $(this).width()) / 2) + $(window).scrollLeft() + "px");
        return $(this);
        //
    }
})(jQuery);

(function ($) {

    $.fn.fadeMessage = function (msg, options) {
        //alert(msg.length);        
        var opts = $.extend({}, $.fn.fadeMessage.defaults, options);
        var msgHtml = "<div id='blackout'></div><div id='fade_message' title='click to close'><div id='fade_message_text'></div></div>";
        var o = $.meta ? $.extend({}, opts, $(this).data()) : opts;

        $(this).prepend(msgHtml);

        $msg = $(this).find("#fade_message");
        $container = $(this).find("#blackout");


        /*
        alert($container.html());
        */
        if (o.bg != 'true') {
            $container.hide();
        }
        else {
            $container.show();
            var height = $(window).height();
            $container.height(height);
        }

//        public enum MessageType
//        {
//            Information,
//            Question,
//            Warning,
//            Error
//        }
        //$msg.css("background-image", "url(" + imagepath + "message-sprite.png)");
        // alert(o.type);

        //Information,
        //    Warning,
        //    Error,
        //    Success

        switch (o.type) {
            case '0':            
            case 'info':
                $msg.addClass("message info24");
                break;
            case '1':
            case 'warning':
                $msg.addClass("message warning24");
                break;            
            case '2':
            case 'error':
                $msg.addClass("message error24");
                break;
            case '3':
            case 'success':
                $msg.addClass("message success24");
                break;
            default:
                $msg.addClass("message info24");
        }

        $msg.css("background-repeat", "no-repeat");

        if (msg.length > 300)
            $msg.width("600px");

        $msg.html(msg);
        $msg.show();
        $msg.center();

        if (o.allowClose == 'true') {
            $msg.click(function () {

                if (o.redirect && o.redirect.length > 0) {
                    if (o.redirect.indexOf("function:") > -1) {
                        var value = o.redirect + "";
                        value = value.replace("function:", ""); // value
                        setTimeout(value, 10);
                    }
                    else
                        document.location = o.redirect;
                }
                else {
                    $msg.hide();
                    $container.hide();
                }

                $container.remove();
                $msg.remove();

            });



        }

        if (o.fadeOut != 'false') {
            $container.hide();
            $msg.animate({ opacity: 1.0 }, o.timeout);
            $msg.fadeOut(o.speed, function () { $container.remove(); $msg.remove(); });
           
        }
        else {
            $container.height($(document).height());
        }
    }

    $.fn.fadeMessage.defaults = {
        type: '0', //information icon 
        fadeOut: 'false',
        bg: 'false', //opaque bg 1 = show
        redirect: '',
        timeout: 2000,
        speed: 1000,
        allowClose: 'true'
    };

})(jQuery);

function displayFadeMessage(msg, type, fadeOut, modalBg, redirect, bck, brdr, allowClose) {

    modalBg = 'true';
    //console.log(type);

    $(document).ready(function () {

        var options = {
            type: type,
            fadeOut: fadeOut,
            bg: modalBg,
            redirect: redirect,
            backgroundcolor: bck,
            bordercolor: brdr,
            allowClose: allowClose
        }
        //alert(msg);
        try
        { $("body").fadeMessage(msg, options); }
        catch (e)
        { alert("FadeMessage Error: " + e.message + "\n" + msg); }



    });

}



