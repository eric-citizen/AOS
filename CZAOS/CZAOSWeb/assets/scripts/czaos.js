$(document).ready(function () {

    //init tabs controls
    tabs();
    preloadImages();

    if ($(".pwd-strength").length > 0) {
        $(".pwd-strength").password_strength();
    }
    
    //wait for ctls based inside table cell/gridview
    $(".cell-wait-click").click(function () {        
        $(this).addClass("cell-wait");
    });

    $(".cell-link-click").click(function () {
        $(this).addClass("cell-link-wait");
    });    
    
    if ($(".focusme").length > 0) {
       
        setTimeout(function () {
            
            $(".focusme").each(function () {                

                if ($(this).attr("readonly") === undefined) {                    

                    if ($(this).val().length === 0)
                    {
                        $(this).focus();
                    }
                    else
                    {

                    }
                    
                }                
                    
            });
           
        }, 400)
    }

    $("table.gridview tr:even").css("background-color", "#dcdcdc");
    $("table.gridview tr:has(td)").hover(
      function () {
          $(this).addClass("hilite-row");
      },
      function () {
          $(this).removeClass("hilite-row");
      }
    );


     
});


function tabs()
{
    /* TABS */
    var oldIndex = 0;
    var index = 'home';

    if ($("#tabs").length > 0) {
        index = $("#tabs").data("name");

        if (index === null) {
            index = 'home';
        }
    }
    //  Define friendly data store name
    var dataStore = window.sessionStorage;
    //  Start magic!
    try {
        // getter: Fetch previous value
        oldIndex = dataStore.getItem(index);

    } catch (e) {
        // getter: Always default to first tab in error state
        oldIndex = 0;
    }
    
     
    if ($("#tabs").hasClass("no-tab-persist")) {
        oldIndex = 0;
    }
  
    $("#tabs").tabs({
        // The zero-based index of the panel that is active (open)
        active: oldIndex,
        // Triggered after a tab has been activated
        activate: function (event, ui) {
            //  Get future value
            var newIndex = ui.newTab.parent().children().index(ui.newTab);
            //  Set future value
            //alert(newIndex);
            if (!$(this).hasClass("no-tab-persist"))
            {
                dataStore.setItem(index, newIndex);                
            }

        }
    });
    /* TABS */
}

function setTabsIndex(index) {

    var tt = setTimeout("$('#tabs').tabs('active', " + index + ");", 300)
    //$('#tabs').tabs('select', index);
}

function removeTab(index) {
    
    // Remove the tab
    setTimeout(function () {
        var tab = $("#tabs").find(".ui-tabs-nav li:eq(" + index + ")").remove();
        // Refresh the tabs widget
        $("tabs").tabs("refresh");
    }, 10);

}

function enableTab(index) {
    var tt = setTimeout("$('#tabs').tabs('enable', " + index + ");", 300)
}

function disableTab(index) {
    var tt = setTimeout("$('#tabs').tabs('disable', " + index + ");", 300)
}

function preloadImages()
{
    jQuery("<img>").attr("src", "/images/progress.gif");
}


