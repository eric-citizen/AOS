﻿<%@ Page Title="Manage Districts" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="districts.aspx.cs" Inherits="CZAOSWeb.admin.schools.districts" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link dialog-link" href="/admin/schools/edit-district.aspx" data-args="600, 700, true, null, 1">Add New District</a>    

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.SchoolDistrictList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvDistricts" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvDistricts" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="DistrictID" OnRowCommand="gvDistricts_RowCommand" OnRowDataBound="gvDistricts_RowDataBound">
        <Columns>

            <asp:BoundField DataField="District" SortExpression="District" HeaderText="District">                
            </asp:BoundField>            
            
            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="600, 700, true, null, 1" Text="Edit" ToolTip="Edit this item" NavigateUrl='<%# Bind("DistrictID","~/admin/schools/edit-district.aspx?dId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("DistrictID") %>' CommandName="DeleteDistrict" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

        <PagerTemplate>&nbsp;</PagerTemplate>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvDistricts" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvDistricts" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>