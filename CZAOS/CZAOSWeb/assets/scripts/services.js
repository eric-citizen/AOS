function extendSession() {    
     
    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/ExtendSession",
        cache: false,        
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {        

        if (result.d.indexOf("success") > -1) {
            if (console && console.log) {
                console.log("ExtendSession success");
            }
        }
        else {
            if (console && console.log)
            {
                console.log("ExtendSession error: " + result.d);
            }
                
        }

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("ExtendSession request Failed: " + " Err: " + err);        
    })
    .always(function () {
        //console.log("ExtendSession finish.");
    });

}

function AnimalRegionExists(controlId, regionId) {
    
    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/RegionExists",
        cache: false,
        data: "{'regionId' :'" + regionId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {

        if (result.d === true) {
            toastr.error("This region Id is already in use.");
            tb = $("#" + controlId);
            tb.val("");
            tb.focus();
        }        

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("RegionExists request Failed: " + " Err: " + err);
    })
    .always(function () {
         
    });

}

function UserNameExists(controlId, name) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/UserExists",
        cache: false,
        data: "{'name' :'" + name + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {

        if (result.d === true) {
            toastr.error("This username is already in use.");
            tb = $("#" + controlId);
            tb.val("");
            tb.focus();
        }

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("UserExists request Failed: " + " Err: " + err);
    })
    .always(function () {

    });

}

function UserEmailExists(controlId, email) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/UserEmailExists",
        cache: false,
        data: "{'email' :'" + email + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {

        if (result.d === true) {
            toastr.error("This email address is already in use.");
            tb = $("#" + controlId);
            tb.val("");
            tb.focus();
        }

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("UserEmailExists request Failed: " + " Err: " + err);
    })
    .always(function () {

    });

}

function TemplateKeyExists(controlId, key, id) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/TemplateKeyExists",
        cache: false,
        data: "{'key' :'" + key + "', 'id' :'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {

        if (result.d === true) {
            toastr.error("This template key is already in use.");
            tb = $("#" + controlId);
            tb.val("");
            tb.focus();
        }

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("TemplateKeyExists request Failed: " + " Err: " + err);
    })
    .always(function () {

    });

}

function FolderExists(controlId, folder) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/FolderExists",
        cache: false,
        data: "{'folder' :'" + folder + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {

        if (result.d === false) {
            toastr.error("This folder does not exist!");
            tb = $("#" + controlId);
            tb.val("");
            tb.focus();
        }

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("FolderExists request Failed: " + " Err: " + err);
    })
    .always(function () {

    });

}

function GetLogFile(resultControlId, filename) {

    //$("#" + resultControlId).load("http://localhost:53637/logs/" + filename);
    //return;

    //$.ajax({
    //    url: "/logs/" + filename,
    //    dataType: "text",
    //    success: function (data) {
    //        $("#" + resultControlId).html(data);
    //    }
    //});
    //return;
    
    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/GetLogFileContents",
        cache: false,
        data: "{'filename' :'" + filename + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {       
        tb = $("#" + resultControlId);
        tb.html(result.d);                
    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        //console.log("GetLogFile request Failed: " + " Err: " + err);
        //alert("GetLogFile request Failed: " + " Err: " + err);
    })
    .always(function () {
        //console.log("GetLogFile complete");
        //alert("GetLogFile complete");
        $("#" + resultControlId).removeClass("cell-wait");
        $("#" + resultControlId).removeClass("hidden");
    });

}

//checkfolderexists

function GetTimeDiff(id, start, end) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/GetTimeDifference",
        cache: false,
        data: "{'start' :'" + start + "', 'end' :'" + end + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
    .done(function (result) {
        console.log(result.d);
        $("#" + id).text(result.d);        

    })
    .fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ', ' + error;
        console.log("GetTimeDiff request Failed: " + " Err: " + err);       
    })
    .always(function () {

    });

}

function GetRegionList() {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/GetRegionList",
        data: "{}",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert('Error: ' + err.Message);
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            console.log(response);

            $('#regions').empty(); // Clear the dd
            //<td><input id="regions_0" type="checkbox" name="ctl00$body$regions$regions_0" value="1"><label for="regions_0">1</label></td>
            // Loop through the list
            $.each(response.d, function (key, val) {

                var row = '<td><a class="detail-link" href="#" data-id=' + val.Id + ' >' + val.Name + '</a></td><td>' + val.Price + '</td>';

                $('<tr/>', { html: row })  // Append the name.
                    .appendTo($('#regions'));
                $('<tr/><td/>', { value: val.AnimalRegionCode, html: val.AnimalRegionName }).appendTo($('#regions'));
            });

            $('#ar-item').show();            

        },
        failure: function (msg) {
            alert('failure: ' + msg);
        }
    });
}

function GetSchoolList(districtId) {    
    
    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/GetSchoolList",
        data: "{'districtId' :'" + districtId + "'}",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert('Error: ' + err.Message);
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            //console.log(response);

            $('#ddSchool').empty(); // Clear the dd
            //<td><input id="regions_0" type="checkbox" name="ctl00$body$regions$regions_0" value="1"><label for="regions_0">1</label></td>
            // Loop through the list
            //console.log(districtId);
            //console.log(response.d);
            
            $.each(response.d, function (key, val) {

                $('#ddSchool')
                     .append($("<option></option>")
                     .attr("value", val.SchoolID)
                     .text(val.SchoolName));
               
            });

            $('#ddSchool').show();

        },
        failure: function (msg) {
            alert('failure: ' + msg);
        }
    });
}

function GetExhibitList(regionId) {

    $.ajax({
        type: "POST",
        url: "/admin/services/services.asmx/GetExhibitList",
        data: "{'regionId' :'" + regionId + "'}",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert('Error: ' + err.Message);
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            //console.log(response);

            $('#ddExhibit').empty(); // Clear the dd            

            $.each(response.d, function (key, val) {

                $('#ddExhibit')
                     .append($("<option></option>")
                     .attr("value", val.ExhibitID)
                     .text(val.ExhibitName));

            });

            $('#region-exhibit-container').show();

        },
        failure: function (msg) {
            alert('failure: ' + msg);
        }
    });
}