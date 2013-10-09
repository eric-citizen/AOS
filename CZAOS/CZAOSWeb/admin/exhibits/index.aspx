<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs"  MasterPageFile="~/masterpages/Main.Master" Inherits="CZAOSWeb.admin.exhibits.index1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <div class="steps">
        <div class="title_bar">
            Add / Modify / Delete Set-Up Data :: <span class="section_name">Exhibits</span>
        </div>
        <div class="step" id="region-knockout-scope">
            <div class="title">Region</div>
            <select data-bind="options: Regions, optionsText: 'AnimalRegionName', value: SelectedRegion"></select>
        </div>
        <div class="step" id="exhibit-knockout-scope">
            <div class="title">Exhibit</div>
            <select data-bind="options: SelectableExhibits, optionsText: 'ExhibitName', value: SelectedExhibit"></select>
        </div>
        <div class="step" id="location-knockout-scope">
            <div class="title">Exhibit Locations</div>
            <div class="add-link ui-dialog-link" href="/admin/location/edit-location.aspx" data-args="500, 700, true, null, 1">Add New Location</div>
            <ul id="step_3" data-bind="foreach: Locations">
                <li style="display: none" data-bind="visible: $root.Display($data)">
                    <span data-bind="text: Location"></span>
                    <span class="ui-dialog-link gv-edit-link" data-args="500, 700, true, null, 1" data-bind="click: $root.Edit, attr: { href: '/admin/location/edit-location.aspx?locId=' + $data.LocationID() }">Edit</span>
                </li>
            </ul>
        </div>
    </div>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
    <script src="/assets/scripts/RegionControl.js"></script>
    <script src="/assets/scripts/ExhibitControl.js"></script>
    <script src="/assets/scripts/LocationControl.js"></script>
    <script language="javascript">
        $(function () {
            window.App.RegionControl().Configure();
            window.App.ExhibitControl().Configure();
            window.App.LocationControl().Configure();
        });
    </script>
</asp:Content>