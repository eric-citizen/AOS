
$(document).ready(function () {

    if ($(".username:first").length > 0 && $(".username:first").val().length > 0) {
        if ($(".password:first").length > 0 && $(".password:first").val().length > 0) {
            $(".cms-button:first").focus();
        }
        else
            $(".password:first").focus();
    }
    else {
        $(".username:first").focus(); $(".username:first").select();
    }

});



