﻿<%@ Page Title="Manage Schools" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="schools.aspx.cs" Inherits="CZAOSWeb.admin.schools.schools" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>
<%@ Register Src="~/controls/SimpleConfirmControl.ascx" TagPrefix="uc1" TagName="SimpleConfirmControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" ClientIDMode="Static" />
    
    <div>
        <h2>School District:&nbsp;<asp:Literal runat="server" ID="litDistrict"></asp:Literal></h2>
    </div>

    <a class="add-link ui-dialog-link" href="/admin/schools/edit-school.aspx?districtId=<%= this.DistrictID %>" title="Add New School" data-width="300" data-height="250" data-rp="1">Add New School</a>        
    <div class="alphabet-container pt10">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    
    
    <div class="clearfix">
        <div class="animalSearch pb5 floatRight">
            <label class="required">Free Text Search:</label>
            <mack:RequiredTextBox runat="server" CssClass="" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
            <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click"/>
            <asp:LinkButton runat="server" CssClass="edit-button button" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
        </div>
    </div>

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.SchoolList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />            
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvSchools" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvSchools" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="10" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="SchoolID" OnRowCommand="gvSchools_RowCommand" OnRowDataBound="gvSchools_RowDataBound">
        <Columns>

            <asp:BoundField DataField="SchoolName" SortExpression="School" HeaderText="School" ItemStyle-Width="200px">                
            </asp:BoundField>            
            <asp:BoundField DataField="DistrictName" SortExpression="DistrictName" HeaderText="District" ItemStyle-Width="200px">                
            </asp:BoundField>  
            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="250, 300, true, null, 1" Text="" ToolTip="Edit School Data" NavigateUrl='<%# Bind("SchoolID","~/admin/schools/edit-school.aspx?schoolId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("SchoolID") %>' CommandName="DeleteSchool" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

        
    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvSchools" HideOnEmpty="False"/>

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvSchools" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
