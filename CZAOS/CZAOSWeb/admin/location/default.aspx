<%@ Page Title="Manage Locations" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.Location._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/location/edit-location.aspx" data-args="325, 350, true, null, 1" title="Add New Location">Add New Location</a>
    <a class="add-link ui-dialog-link" href="/admin/location/sort-location.aspx" data-args="600, 500, true, null, 1" title="Edit Sort Order">Edit Sort Order</a>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.LocationList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvLocation" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvLocation" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="LocationID" OnRowCommand="gvLocation_RowCommand" OnRowDataBound="gvLocation_RowDataBound">
        <Columns>

            <asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description">                
            </asp:BoundField>
            
            <asp:BoundField DataField="LocationCode" SortExpression="LocationCode" HeaderText="Code" HeaderStyle-CssClass="tar">
                <ItemStyle Width="50px" CssClass="tar" />
            </asp:BoundField> 

            <asp:BoundField DataField="maskAMA" SortExpression="maskAMA" HeaderText="MaskAma">
                <ItemStyle Width="50px" CssClass="tac" />
            </asp:BoundField> 
            <asp:BoundField DataField="maskProf" SortExpression="maskProf" HeaderText="MaskProf">
                <ItemStyle Width="50px" CssClass="tac" />
            </asp:BoundField> 

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="325, 300, true, null, 1" Text="" ToolTip="Edit Location" NavigateUrl='<%# Bind("LocationID","~/admin/location/edit-location.aspx?locId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("LocationID") %>' CommandName="DeleteLocation" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvLocation" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvLocation" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
