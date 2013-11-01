<%@ Page Title="Manage Animal Regions" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.AnimalRegions._default" %>

<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--    <a class="add-link ui-dialog-link" href="/admin/animal-regions/edit-region.aspx" data-args="300, 300, true, null, 1" title="Add New Region">Add New Region</a>--%>

    <div class="animalSearch">
        <label class="required">Free Text Search:</label>
        <mack:RequiredTextBox runat="server" CssClass="" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
        <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click" />
        <asp:LinkButton runat="server" CssClass="edit-button button" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
    </div>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.AnimalRegionList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">

        <SelectParameters>
            <asp:Parameter Name="filterExpression" Type="string" DefaultValue="" />
        </SelectParameters>

    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvAnimalRegions" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvAnimalRegions" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false"
        DataKeyNames="AnimalRegionCode" OnRowDataBound="gvAnimalRegions_RowDataBound" OnRowCommand="gvAnimalRegions_RowCommand">
        <Columns>

            <asp:BoundField DataField="AnimalRegionCode" SortExpression="AnimalRegionCode" HeaderText="Code">
                <ItemStyle Width="120px" />
            </asp:BoundField>

            <asp:BoundField DataField="AnimalRegionName" SortExpression="AnimalRegion" HeaderText="Animal Region">
                <ItemStyle Width="220px" />
            </asp:BoundField>

            <asp:BoundField DataField="RegionName" SortExpression="RegionName" HeaderText="Region Name"></asp:BoundField>

            <asp:BoundField DataField="AnimalCount" SortExpression="AnimalCount" HeaderText="Animal Count" ItemStyle-CssClass="tar" HeaderStyle-CssClass="tar">
                <ItemStyle Width="100px" />
            </asp:BoundField>

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="60px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>

            <%--            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="300, 300, true, null, 1" Text="" ToolTip="Edit Animal Region" NavigateUrl='<%# Bind("AnimalRegionCode","~/admin/animal-regions/edit-region.aspx?regionId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>--%>

            <%--            <asp:TemplateField ShowHeader="False" >
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("AnimalRegionCode") %>' CommandName="DeleteRegionCode" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>--%>
        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvAnimalRegions" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvAnimalRegions" Text="No records found!"></mack:MessageDiv>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
