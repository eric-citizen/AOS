<%@ Page Title="Manage Behaviors" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CZAOSWeb.admin.Behavior._default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <div class="steps">
        <div class="title_bar">
            Add / Modify / Delete Set-Up Data :: <span class="section_name">Behaviors</span>
        </div>
        <div class="step" id="region-knockout-scope">
            <div class="title">Region</div>
            <select data-bind="options: Regions, optionsText: 'AnimalRegionName', value: SelectedRegion"></select>
        </div>
        <div class="step" id="exhibit-knockout-scope">
            <div class="title">Exhibit</div>
            <select data-bind="options: SelectableExhibits, optionsText: 'ExhibitName', value: SelectedExhibit"></select>
        </div>
        <div class="step" id="behavior-category-knockout-scope">
            <div class="title">Behavior Category</div>
            <div class="add-link ui-dialog-link" href="/admin/location/edit-behavior-category.aspx" data-args="500, 700, true, null, 1">Add New Behavior Category</div>
            <%--<select data-bind="options: Categories, optionsText: 'Description', value: SelectedCategory"></select>--%>
            <ul id="step_3" data-bind="foreach: Categories">
                <li>
                    <span data-bind="text: Description"></span>
                    <span class="ui-dialog-link gv-edit-link" data-args="500, 700, true, null, 1" data-bind="click: $root.Edit, attr: { href: '/admin/location/edit-behavior-category.aspx?bcatId=' + $data.BvrCatID() }">Edit</span>
                </li>
            </ul>
        </div>
        <div id="behavior-knockout-scope">
            <div class="step">
                <div class="title">Behavior</div>
                <div class="add-link ui-dialog-link" href="/admin/location/edit-location.aspx" data-args="500, 700, true, null, 1">Add New Location</div>
                <ul id="Ul1" data-bind="foreach: Behaviors">
                    <li style="display: none" data-bind="visible: $root.Display($data)">
                        <span data-bind="text: BehaviorName"></span>
                        <span class="ui-dialog-link gv-edit-link" data-args="500, 700, true, null, 1" data-bind="click: $root.Edit, attr: { href: '/admin/location/behaviors.aspx?bcatId=' + $data.BehaviorID() }">Edit</span>
                    </li>
                </ul>
            </div>
            <div class="step" id="behavior-detail">
                <div class="title">Detail</div>
                <ul>
                    <li data-bind="text: SelectedBehavior().Description"></li>
                </ul>
            </div>
        </div>

    </div>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
    <script src="/assets/scripts/RegionControl.js"></script>
    <script src="/assets/scripts/ExhibitControl.js"></script>
    <script src="/assets/scripts/RegionControl.js"></script>
    <script language="javascript">
        $(function () {
            window.App.RegionControl().Configure();
        });
    </script>
</asp:Content>