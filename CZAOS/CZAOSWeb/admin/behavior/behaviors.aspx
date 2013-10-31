<%@ Page Title="Manage Animals" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="behaviors.aspx.cs" Inherits="CZAOSWeb.admin.Behavior.behaviors" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" ClientIDMode="Static" />
<%--    <a class="sort-link ui-dialog-link" href="/admin/behavior/sort-behavior.aspx?bcatId=<%= this.CategoryID %>" data-args="600, 500, true, null, 1">Edit Sort Order</a>--%>
    
    <div>
        <h2>Behavior Category:&nbsp;<asp:Literal runat="server" ID="litCatName"></asp:Literal></h2>
    </div>

    <a class="add-link ui-dialog-link" href="/admin/behavior/edit-behavior.aspx?bcatId=<%= this.CategoryID %>" data-args="400, 450, true, null, 1">Add New Behavior</a>
    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.BehaviorList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvBCat" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvBCat" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="10" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="BehaviorID" OnRowDataBound="gvBCat_RowDataBound" OnRowCommand="gvBCat_RowCommand">
        <Columns>

            <asp:BoundField DataField="BehaviorName" SortExpression="BehaviorName" HeaderText="Behavior">                
            </asp:BoundField>
            
            <asp:BoundField DataField="BehaviorCode" SortExpression="BehaviorCode" HeaderText="Code">
                <ItemStyle Width="50px" />
            </asp:BoundField> 

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="350, 450, true, null, 1" Text="" ToolTip="Edit this item" ></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("BehaviorID") %>' CommandName="DeleteBehavior" />
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
