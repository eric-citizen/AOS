<%@ Page Title="Manage Animals" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.animals.index" %>
<%@ Import Namespace="System.Web.Http.Routing" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="steps">
        <div class="title_bar">
            Add / Modify / Delete Set-Up Data :: <span class="section_name">Animals</span>
            <nav class="menu_drop">
                <ul>
                    <li><a href="Animals">Animals</a></li>
                    <li><a href="Habitats">Habitats</a></li>
                </ul>
            </nav>
        </div>
        <div class="step" id="region-knockout-scope">
            <div class="title">Region</div>
            <div class="add-link ui-dialog-link" href="/admin/animal-regions/edit-region.aspx" data-args="270, 600, true, null, 1">Add New Region</div>
            <ul id="step_1" data-bind="foreach: Regions">
                <li style="display: none" data-bind="visible: Active">
                    <span data-bind="text: AnimalRegionName, click: $root.UpdateSelectedRegion"></span>
                    <span class="ui-dialog-link gv-edit-link" data-args="270, 600, true, null, 1" data-bind="click: $root.Edit, attr: { href: '/admin/animal-regions/edit-region.aspx?regionId=' + $data.AnimalRegionCode() }">Edit</span>
                </li>
            </ul>
        </div>
        <div class="step" id="exhibit-knockout-scope">
            <div class="title">Exhibit</div>
            <div class="add-link ui-dialog-link" href="/admin/exhibits/edit-exhibit.aspx" data-args="350, 700, true, null, 1" data-bind="enable: AddEnabled()">Add New Exhibit</div>
            <ul id="step_2" data-bind="foreach: Exhibits">
                <li style="display: none" data-bind="visible: $root.Display($data)">
                    <span data-bind="text: ExhibitName, click: $root.UpdateSelectedExhibit"></span>
                    <span class="ui-dialog-link gv-edit-link" data-args="250, 700, true, null, 1" data-bind="click: $root.Edit, attr: {href: '/admin/exhibits/edit-exhibit.aspx?exId=' + $data.ExhibitID() }">Edit</span>
                </li>
            </ul>
        </div>
        <div class="step" id="animal-knockout-scope">
            <div class="title">Animal</div>
            <div class="add-link ui-dialog-link" href="/admin/animals/edit-animal.aspx" data-args="500, 700, true, null, 1" data-bind="enable: AddEnabled()">Add New Animal</div>
            <ul id="step_3" data-bind="foreach: Animals">
                <li style="display: none" data-bind="visible: $root.Display($data)">
                    <span data-bind="text: CommonName, click: $root.UpdateSelectedAnimal"></span>
                    <span class="ui-dialog-link gv-edit-link" data-args="500, 700, true, null, 1" data-bind="click: $root.Edit, attr: { href: '/admin/animals/edit-animal.aspx?animalId=' + $data.AnimalID() }">Edit</span>
                </li>
            </ul>
        </div>
    </div>
    

</asp:Content>

<asp:Content ContentPlaceHolderID="scripts" runat="server">
    <script src="/assets/scripts/AnimalControl.js"></script>
    <script language="javascript">
        $(function() {
            window.App.RegionControl().Configure();
            window.App.ExhibitControl().Configure();
            window.App.AnimalControl().Configure();
        });
    </script>
</asp:Content>