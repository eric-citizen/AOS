<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="sort-location.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.sort_location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            $("#sortable").sortable();
            $("#sortable").disableSelection();
        });

        $(function () {
            $("#sortable").sortable({
                update: function (event, ui) {
                    var newOrder = $(this).sortable('toArray').toString();
                    UpdateSortOrder(newOrder);
                }
            });

            $("#sortable").disableSelection();

        });

        function resetList() {

            $('#sortable li').each(function (index) {
                $(this).attr("id", "body_rptSortItems_liSortItem_" + index);
            });
        }

        function UpdateSortOrder(sortedIds) {

            var API_URL = "sort-location.aspx/SaveSortOrder";           

            $.ajax({
                url: API_URL,
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: "{'sortedIds' : '" + sortedIds + "'}",
                statusCode: {
                    500: function (data) {
                        if (console && console.log) {
                            console.log("Error 500 " + data.Message);
                        }
                    }
                }

            })
            .done(function (data) {
                if (console && console.log) {
                    console.log("sort order updated successfully");
                }
                toastr.success("sort order updated successfully");
                $("#sortable").sortable("refreshPositions");
                $("#sortable").sortable("refresh");
                //$("#btnSaveSort").click();
                resetList();

                //__doPostBack('upAltImage', '');
            })
            .fail(function (jqxhr, textStatus, error) {
                if (console && console.log) {
                    var err = textStatus + ', ' + error;
                    console.log("Request Failed: " + " Err: " + err);
                }
                toastr.success("Request Failed: " + " Err: " + err);
            })
            .always(function () {

            });


        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="Static"  />

    <fieldset class="form-fieldset">
        
        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Location Sort Order</asp:Literal>
        </legend>   

        <ul id="sortable">

            <asp:Repeater runat="server" ID="rptSortItems" OnItemDataBound="rptSortItems_ItemDataBound">
                <ItemTemplate>
                    <li runat="server" id="liSortItem" class="ui-state-default">
                        <span style="display:inline-block;" class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                        <asp:Literal runat="server" ID="litName" Text='<%# Bind("Description") %>'></asp:Literal>
                    </li>
                </ItemTemplate>
            </asp:Repeater>         
          
        </ul>        
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>

        

    </script>

</asp:Content>
