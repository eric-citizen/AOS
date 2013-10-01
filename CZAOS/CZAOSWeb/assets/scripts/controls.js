/* grid delete row */
$(".del-link").click(function (index) {

    $(".del-row-item").each(function (index) {
        $(this).prev().show();
        $(this).hide();
    });

    $(this).next().show();
    $(this).hide();
});

$(".del-cancel").click(function (index) {
    $(this).parent().prev().show();
    $(this).parent().hide();
});
/* grid delete row */

/* EDITABLE TEXTBOX/LABEL */
$(".etb-link").click(function (index) {

    $(".etb-box").val("");

    $(".etb-editable").each(function (index) {
        $(this).prev().show();
        $(this).hide();
    });

    $(this).next().show();
    $(this).next().find(".etb-box").val($(this).html());
    $(this).next().find(".etb-box").select();
    $(".etb-box").focus();
    $(this).hide();
});

$(".etb-cancel").click(function (index) {
    $(".etb-box").val("");
    $(this).parent().prev().show();
    $(this).parent().hide();
});

$(".etb-update").click(function (index) {
    //$(this).html("wait...");
    if ($(this).prev().val().length > 0) {
        $(this).parent().addClass("div-wait");
    }
});

/* EDITABLE TEXTBOX/LABEL */


