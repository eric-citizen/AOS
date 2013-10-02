<%@ Page Title="Manage Animals" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CZAOSWeb.admin.animals.index" %>

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
            <ul id="step_1" data-bind="foreach: Regions">
                <li data-bind="text: AnimalRegionName, click: $root.UpdateSelectedRegion, visible: Active"></li>
            </ul>
        </div>
        <div class="step" id="exhibit-knockout-scope">
            <div class="title">Exhibit</div>
            <ul id="step_2" data-bind="foreach: Exhibits">
                <li data-bind="text: ExhibitName, click: $root.UpdateSelectedExhibit, visible: $root.Display($data)"></li>
            </ul>
        </div>
        <div class="step" id="animal-knockout-scope">
            <div class="title">Animal</div>
            <ul id="step_3" data-bind="foreach: Animals">
                <li data-bind="text: CommonName, click: $root.UpdateSelectedAnimal, visible: $root.Display($data)"></li>
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