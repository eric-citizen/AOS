﻿<%@ Page Title="Manage Exhibit Locations" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="locations.aspx.cs" Inherits="CZAOSWeb.admin.Behavior.exhibit_locations" %>
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

    <div class="alphabet-container pt10">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <div class="animalSearch floatRight pb5">
        <label class="required">Free Text Search:</label>
        <mack:RequiredTextBox runat="server" CssClass="" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
        <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click"/>
        <asp:LinkButton runat="server" CssClass="edit-button button" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
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

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvExhibitLocations" CssClass="w750" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
