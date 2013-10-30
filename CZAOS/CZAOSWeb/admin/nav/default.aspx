<%@ Page Title="Manage Admin Navigation" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.master.nav" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/SimpleConfirmControl.ascx" TagPrefix="uc1" TagName="SimpleConfirmControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/nav/edit-nav.aspx" title="Add New Nav Item" data-width="400" data-height="425" data-rp="1">Add Nav Item</a>   
    <%--<uc1:SimpleConfirmControl runat="server" ID="sccInit" Content="Are you sure? Current nav records will be erased!" LinkText="Initialize Nav" Title="Initialize Nav" OnConfirm="sccInit_Confirm" />--%>
    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.AdminNavigationList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />            
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvData" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvData" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="ID" OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_RowDataBound">
        <Columns>

            <asp:BoundField DataField="Folder" SortExpression="Folder" HeaderText="Folder" ItemStyle-Width="150px">                
            </asp:BoundField> 
            <asp:BoundField DataField="Roles" HeaderText="Roles">                
            </asp:BoundField> 
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link edit" data-args="425, 400, true, null, 1" Text="" ToolTip="Edit Condition" NavigateUrl='<%# Bind("ID","~/admin/nav/edit-nav.aspx?navId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ID") %>' CommandName="DeleteNav" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

        
    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvData" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvData" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
