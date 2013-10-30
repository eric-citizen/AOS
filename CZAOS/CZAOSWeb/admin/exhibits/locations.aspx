<%@ Page Title="Manage Exhibit Locations" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="locations.aspx.cs" Inherits="CZAOSWeb.admin.Behavior.exhibit_locations" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnExhibitID" ClientIDMode="Static" />

    <div>
        <h2>Exhibit:&nbsp;<asp:Literal runat="server" ID="litExhibitName"></asp:Literal></h2>
    </div>

    <a class="add-link ui-dialog-link" href="/admin/exhibits/edit-exhibit-location.aspx?exId=<%= this.ExhibitID %>" data-args="225, 250, true, null, 1" title="Add New Exhibit Location">Add New Exhibit Location</a>        

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.ExhibitLocationList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvExhibitLocations" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvExhibitLocations" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="10" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvExhibitLocations_RowDataBound" OnRowCommand="gvExhibitLocations_RowCommand"
        DataKeyNames="ExhibitLocationID" PagerSettings-Visible="false"  >
        <Columns>
            
            <asp:BoundField DataField="Location" SortExpression="Location" HeaderText="Location">                
            </asp:BoundField> 

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ExhibitLocationID") %>' CommandName="DeleteLocation" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvExhibitLocations" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvExhibitLocations" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
