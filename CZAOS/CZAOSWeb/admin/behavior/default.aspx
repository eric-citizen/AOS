<%@ Page Title="Manage Behavior Categories" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.Behavior._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/behavior/edit-behavior-category.aspx" data-args="400, 700, true, null, 0" title="Add New Behavior Category">Add New Behavior Category</a>
    <a class="add-link ui-dialog-link" href="/admin/behavior/sort-behavior-category.aspx" data-args="600, 700, true, null, 0">Edit Sort Order</a>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.BehaviorCategoryList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvBCat" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvBCat" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="BvrCatID" OnRowDataBound="gvBCat_RowDataBound" OnRowCommand="gvBCat_RowCommand">
        <Columns>

            <asp:BoundField DataField="BvrCat" SortExpression="BvrCat" HeaderText="Category Name">
                <ItemStyle Width="100px" />                
            </asp:BoundField>
            
            <asp:BoundField DataField="BvrCatCode" SortExpression="BvrCatCode" HeaderText="Code">
                <ItemStyle Width="60px" />
            </asp:BoundField> 

            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Behavior Count" SortExpression="BehaviorCount">                    
                <ItemTemplate>                    
                    <asp:HyperLink runat="server" ID="lnkBehaviorEdit" CssClass="ui-dialog-link" data-args="775, 650, true, null, 1" Text='<%#Bind("BehaviorCount") %>' ToolTip="Edit this category's behaviors" NavigateUrl='<%# Bind("BvrCatID","~/admin/behavior/behaviors.aspx?bcatId={0}") %>'></asp:HyperLink>                
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="400, 400, true, null, 1" Text="" ToolTip="Edit this item" NavigateUrl='<%# Bind("BvrCatID","~/admin/behavior/edit-behavior-category.aspx?bcatId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("BvrCatID") %>' CommandName="DeleteCategory" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>        

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvBCat" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvBCat" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
