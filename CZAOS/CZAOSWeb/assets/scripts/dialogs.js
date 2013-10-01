/// <reference path="libs/simplemodal/js/jquery.simplemodal.1.4.4.min.js" />
/* File Created: April 24, 2013 */
var key = 'loadkey';

var CZAOSDialogs = new function () {

    this.CloseModal = function () {
        $.modal.close();
    };   

    this.ShowDialog = function (url, hgt, wd, oc, controlId, refreshParent) {

        hgt = assignDefaultValue(hgt, 400);
        wd = assignDefaultValue(wd, 650);
        oc = assignDefaultValue(oc, true);
        controlId = assignDefaultValue(controlId, "");
        refreshParent = assignDefaultValue(refreshParent, "0");
        
        var cwd = wd + 10;
        //alert(controlId);

        if (controlId != 'null' && controlId.length > 0) {
            url = url + "?controlid=" + controlId;
        }

        $.modal('<iframe src="' + url + '" height="' + hgt + '"px width="' + cwd + '"px style="border:none;overflow-y: hidden!important;overflow-x: auto!important;"  />', {
            containerCss: {
                backgroundColor: "#fff",
                borderColor: "#dcdcdc",
                height: hgt,
                padding: 0,
                width: cwd + "px",
                backgroundImage: "none"
            },
            appendTo: "form",
            overlayClose: oc,
            autoResize: true,
            onShow: function (dialog) {
                resizeSimpleModal();
            },
            onClose: function (dialog) {
                if (refreshParent === "1") {
                    window.top.location.href = window.top.location.href;
                };

                setTimeout(function () {
                    $.modal.close();
                }, 3000);
                
            }
        });


    };

    this.ShowConfirmDialog = function (controlId) {


        $("#" + controlId).modal({
            containerCss: {
                backgroundColor: "#fff",
                borderColor: "#dcdcdc",
                height: "170px",
                padding: 0,
                width: "350px",
                backgroundImage: "none"
            },
            appendTo: "form",
            overlayClose: false,
            autoResize: true,
            onShow: function (dialog) {
                resizeSimpleModal();
                //$(".modalCloseImg").hide();
            },
            onClose: function (dialog) {
                //if (refreshParent === 1) {
                //    window.top.location.href = window.top.location.href;
                //};
                $.modal.close();
            }

        });


    };

    this.ShowSimpleMessage = function (controlId) {


        $("#" + controlId).modal({
            containerCss: {
                backgroundColor: "#fff",
                borderColor: "#dcdcdc",                
                padding: "10px",
                width: "310px",
                backgroundImage: "none"
            },
            appendTo: "form",
            overlayClose: true,
            autoResize: true,
            onShow: function (dialog) {
                resizeSimpleModal();
            },
            onClose: function (dialog) {
                $.modal.close();
            }

        });


    };

    $(".sm-modal-value").click(function () {

        var id = $(this).data("control-id");
        var v = $("#" + id).val();

        var targetId = getParameterByName("controlid");
        
        $('#' + targetId, window.parent.document).val(v);

        window.parent.CZAOSDialogs.CloseModal();

    });

    $(".sm-dialog-link").click(function (event) {

        event.preventDefault();
        CZAOSDialogs.ShowDialogFromArgs($(this));

    });

    $(".xscc-confirm").click(function () {

        CZAOSDialogs.ShowConfirmDialog($(this).data("control-id"));
    });
    
    this.ShowDialogFromArgs = function (link) {
        var args = link.data("args");
        var argArray = args.split(",");
        var url = link.attr("href");        
        var height = assignDefaultValue(argArray[0], 400);
        var width = assignDefaultValue(argArray[1], 500);
        var oc = assignDefaultValue(argArray[2], false);
        var cid = assignDefaultValue(argArray[3], "");
        var rp = assignDefaultValue(argArray[4], "0");

        CZAOSDialogs.ShowDialog(url, parseInt(height), parseInt(width), oc, cid, rp);
    };

    $(".sm-content").click(function () {

        var id = $(this).data("content-id");
        var w = $(this).data("sm-width");
        var h = $(this).data("sm-height");
        h = assignDefaultValue(h, 'auto');

        $("#" + id).modal({
            appendTo: "form",
            containerCss: { height: h, width: w },
            autoResize: true,
            onShow: function (dialog) {
                resizeSimpleModal();
            }
        });

    });

    var resizeSimpleModal = function () {
        
        $("#simplemodal-container").css('height', 'auto'); //To reset the container.
        $(window).trigger('resize.simplemodal');           //To refresh the modal dialog.
        
    };

    var assignDefaultValue = function (value, def) {

        if (value == null || value == "undefined") {
            value = def;
        }

        if (jQuery.type(value) === "string") {
            value = $.trim(value);
        }        

        return value;

    };

    var closeSimpleModal = function () {

        //window.parent.CloseModal();

    };

    var getParameterByName = function (name) {        

        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.search);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));

    };   

    

};

var CZAOSUIDialogs = new function () {

    this.ShowDialog = function (title, url, hgt, wd, controlId, refreshParent) {
         
        if (controlId != 'null' && controlId.length > 0) {
            url = url + "?controlid=" + controlId;
        }

        title = assignDefaultValue(title, "CZAOS Dialog");

        var iframe = $('<iframe frameborder="0" marginwidth="0" marginheight="0" allowfullscreen></iframe>');
        var dialog = $("<div id='div-dialog'></div>").append(iframe).appendTo("body").dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            width: "auto",
            height: "auto",
            close: function () {
                iframe.attr("src", "");
                if (refreshParent === "1") {
                    //parent.location.reload();
                    location.reload();
                };
            }
        });

        iframe.attr({
            width: +wd,
            height: +hgt,
            src: url
        });

        dialog.dialog("option", "title", title).dialog("open");

        //$(".ui-dialog-close").click(function (event) {
        //    event.preventDefault();
        //    $("#" + controlId).dialog('close');
        //});        

    };

    //for SimpleConfirmControl.ascx
    this.ShowSimpleMessage = function (controlId, title) {

        title = assignDefaultValue(title, "CZAOS Dialog");
        var dialog = $("#" + controlId).dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            //width: "auto",
            //height: "auto",           
            close: function () {
            }
        });

        dialog.dialog("option", "title", title).dialog("open");
        
    };

    this.ShowDialogFromArgs = function (link) {

        var src = link.attr("href");
        var args = link.data("args");
        var title = assignDefaultValue(link.attr("title"), link.text());

        if (args != undefined && args.length > 0)
        {
            var argArray = args.split(",");           
            var height = assignDefaultValue(argArray[0], 400);
            var width = assignDefaultValue(argArray[1], 500);
            var oc = assignDefaultValue(argArray[2], false);
            var cid = assignDefaultValue(argArray[3], "");
            var rp = assignDefaultValue(argArray[4], "0");          

        }
        else
        {
            //var title = assignDefaultValue(link.attr("data-title"), "auto");
            var width = assignDefaultValue(link.attr("data-width"), "auto");
            var height = assignDefaultValue(link.attr("data-height"), "auto");
            var cid = assignDefaultValue(link.attr("data-control-id"), "");
            var rp = assignDefaultValue(link.attr("data-rp"), "0");
        }       

        CZAOSUIDialogs.ShowDialog(title, src, parseInt(height), parseInt(width), cid, rp);

    };

    this.closeUIDialog = function closeUIDialog() {        
        $(".ui-dialog-content").dialog().dialog("close");
    }

    //to close from within an iframe
    this.dialogCloseUIDialog = function closeUIDialog() {
        window.parent.CZAOSUIDialogs.closeUIDialog();        
    }

    var assignDefaultValue = function (value, def) {

        if (value == null || value == "undefined") {
            value = def;
        }

        if (jQuery.type(value) === "string") {
            value = $.trim(value);
        }

        return value;

    };

    var getParameterByName = function (name) {

        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.search);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));

    };

    this.refreshParent = function () {
        //parent refresh will close dialog
        //var t = setTimeout(function () { parent.location.reload(); }, 2000)
        var t = setTimeout(function () { parent.location.reload(); }, 1500);
    };    

    $(".ui-modal-value").click(function (event) {

        event.preventDefault();

        var id = $(this).data("control-id");
        var v = $("#" + id).val();

        var targetId = getParameterByName("controlid");

        $('#' + targetId, window.parent.document).val(v);

        window.parent.CZAOSUIDialogs.closeUIDialog();

    });

    $(".ui-dialog-link").click(function (event) {

        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(this));

    });

    $(".ui-scc-confirm").click(function (event) {

        event.preventDefault();
        CZAOSUIDialogs.ShowSimpleMessage($(this).data("control-id"), $(this).data("title"));

    });    
    
    $(".ui-content").click(function (event) {

        event.preventDefault();

        var id = $(this).data("content-id");
        id = $(this).attr("href");
        t = assignDefaultValue($(this).attr("title"), '');

        CZAOSUIDialogs.ShowSimpleMessage(id, t);       

    });

    //$(".ui-dialog-close").click(function (event) {
    //    //alert('xxx');
    //    event.preventDefault();
    //    CZAOSUIDialogs.closeUIDialog();
    //    //window.parent.CZAOSUIDialogs.closeUIDialog();        

    //});



    //JQuery.UI Dialog this.ShowUIDialog =
    $(function () {
        var iframe = $('<iframe frameborder="0" marginwidth="0" marginheight="0" allowfullscreen></iframe>');
        var dialog = $("<div id='div-dialog'></div>").append(iframe).appendTo("body").dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            width: "auto",
            height: "auto",
            close: function () {
                iframe.attr("src", "");
            }
        });

        $("a.Xdialog-link").on("click", function (e) {
            e.preventDefault();
            var src = $(this).attr("href");
            var title = $(this).attr("data-title");
            var width = $(this).attr("data-width");
            var height = $(this).attr("data-height");
            iframe.attr({
                width: +width,
                height: +height,
                src: src
            });
            dialog.dialog("option", "title", title).dialog("open");
        });

    });

};

function refreshParentDelay(delay) {

    if (delay == null) {
        delay = 2000;
    }

    setTimeout(function () {
        //parent.parent.location.href = parent.parent.location.href;
        parent.location.reload();
    }, delay);

}

