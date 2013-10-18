<%@ Page Title="Manage Exhibit Behaviors" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="behaviors.aspx.cs" Inherits="CZAOSWeb.admin.Behavior.exhibit_behaviors" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnExhibitID" ClientIDMode="Static" />
    <a class="add-link ui-dialog-link" href="/admin/exhibits/edit-exhibit-behavior.aspx?exId=<%= this.ExhibitID %>" data-args="250, 600, true, null, 1" title="Add New Exhibit Behavior">Add New Exhibit Behavior</a>        

    <div>
        <b>Exhibit:</b><asp:Literal runat="server" ID="litExhibitName"></asp:Literal>
    </div>

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.ExhibitBehaviorList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvExhibitBehaviors" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvExhibitBehaviors" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvExhibitBehaviors_RowDataBound" OnRowCommand="gvExhibitBehaviors_RowCommand"
        DataKeyNames="ExhibitBehaviorID" PagerSettings-Visible="false" >
        <Columns>
            
            <asp:BoundField DataField="BvrCatID" SortExpression="BvrCatID" HeaderText="Behavior Category" />
            <asp:BoundField DataField="Behavior" SortExpression="Behavior" HeaderText="Behavior">                
            </asp:BoundField> 

            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ExhibitBehaviorID") %>' CommandName="DeleteBehavior" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvExhibitBehaviors" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvExhibitBehaviors" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
