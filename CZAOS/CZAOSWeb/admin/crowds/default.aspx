<%@ Page Title="Manage Crowds" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.Crowds._default" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/crowds/edit-crowd.aspx" data-args="250, 700, true, null, 1" title="Add New Crowd">Add New Crowd</a>        

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" 
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.CrowdList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvCrowds" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvCrowds" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="CrowdID" OnRowCommand="gvCrowds_RowCommand" OnRowDataBound="gvCrowds_RowDataBound">
        <Columns>

            <asp:BoundField DataField="CrowdName" SortExpression="CrowdName" HeaderText="Crowd">                
            </asp:BoundField>            
            
            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="250, 700, true, null, 1" Text="Edit" ToolTip="Edit Crowd" NavigateUrl='<%# Bind("CrowdID","~/admin/crowds/edit-crowd.aspx?cId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("CrowdID") %>' CommandName="DeleteCrowd" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvCrowds" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvCrowds" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
