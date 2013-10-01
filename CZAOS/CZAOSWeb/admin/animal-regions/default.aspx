<%@ Page Title="Manage Animal Regions" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.AnimalRegions._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    dial
    <a class="add-link ui-dialog-link" href="/admin/animal-regions/edit-region.aspx" data-args="270, 600, true, null, 1" title="Add New Region">Add New Region</a>

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.AnimalRegionList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
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

            <asp:BoundField DataField="AnimalRegionName" SortExpression="AnimalRegionName" HeaderText="Animal Region">
                <ItemStyle Width="220px" />
            </asp:BoundField>

            <asp:BoundField DataField="RegionName" SortExpression="RegionName" HeaderText="Region Name">                
            </asp:BoundField>

            <asp:BoundField DataField="AnimalCount" SortExpression="AnimalCount" HeaderText="Animal Count" ItemStyle-CssClass="tar" HeaderStyle-CssClass="tar">   
                <ItemStyle Width="100px" />             
            </asp:BoundField>

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="60px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="270, 600, true, null, 1" Text="Edit" ToolTip="Edit Animal Region" NavigateUrl='<%# Bind("AnimalRegionCode","~/admin/animal-regions/edit-region.aspx?regionId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False" >
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("AnimalRegionCode") %>' CommandName="DeleteRegionCode" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>        

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvAnimalRegions" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvAnimalRegions" Text="No records found!"></mack:MessageDiv>


     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
