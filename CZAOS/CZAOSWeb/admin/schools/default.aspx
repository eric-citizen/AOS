﻿<%@ Page Title="Manage Districts" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.schools._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/schools/edit-district.aspx" data-args="175, 300, true, null, 1">Add New District</a>    
    <br />

    <div class="animalSearch">
        <label class="required">Free Text Search:</label>
        <mack:RequiredTextBox runat="server" CssClass="" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
        <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click"/>
        <asp:LinkButton runat="server" CssClass="edit-button button" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
    </div>

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
            
            <asp:TemplateField ItemStyle-Width="110px" ItemStyle-CssClass="tac">                    
                <ItemTemplate>
                    <asp:HyperLink runat="server" CssClass="edit-button ui-dialog-link"  data-args="800, 700, true, null, 0" ID="btnSchools" AutoPostBack="true" Text="Edit Schools" NavigateUrl='<%# Bind("DistrictID","~/admin/schools/schools.aspx?dID={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="175, 250, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("DistrictID","~/admin/schools/edit-district.aspx?dId={0}") %>'></asp:HyperLink>
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

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvDistricts" HideOnEmpty="False"/>

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvDistricts" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>
    </script>

</asp:Content>
