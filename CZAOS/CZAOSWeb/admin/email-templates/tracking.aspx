<%@ Page Title="Email Tracking" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="tracking.aspx.cs" Inherits="CZAOSWeb.admin.email_templates.tracking" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>
<%@ Register Src="~/controls/AlphabetFilter.ascx" TagPrefix="uc1" TagName="AlphabetFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">    

    <div class="alphabet-container">  
        <uc1:AlphabetFilter runat="server" id="AlphabetFilter" OnAlphabetSelected="AlphabetFilter_AlphabetSelected" />
    </div>    

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.EmailTrackingList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvEmailTemplate" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvEmailTemplate" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" OnRowCommand="gvEmailTemplate_RowCommand" OnRowDataBound="gvEmailTemplate_RowDataBound" PagerSettings-Visible="false" 
        DataKeyNames="ID">
        <Columns>

            <asp:BoundField DataField="SendDate" SortExpression="SendDate" HeaderText="Sent" ItemStyle-Width="140px">                
            </asp:BoundField> 
            <asp:BoundField DataField="To" SortExpression="To" HeaderText="Sent To" ItemStyle-Width="300px">                
            </asp:BoundField>           
            <asp:BoundField DataField="Subject" SortExpression="Subject" HeaderText="Subject">                
            </asp:BoundField>      
            <asp:TemplateField HeaderText="Sent OK">
                <ItemTemplate>
                   <asp:Image runat="server" ID="imgSent" ToolTip="Sent Successfully" ImageUrl="~/images/Actions-dialog-ok-apply-icon-16.png" />
                   <asp:Image runat="server" ID="imgFail" ToolTip='<%#Bind("FailReason") %>' ImageUrl="~/images/messages/Sign-Error-icon-16.png" />
                </ItemTemplate> 
                <ItemStyle Width="50px" CssClass="tac" />               
            </asp:TemplateField>
            <asp:BoundField DataField="OpenDate" SortExpression="OpenDate" HeaderText="Opened" ItemStyle-Width="140px">                
            </asp:BoundField> 
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkView" CssClass="ui-dialog-link gv-view-link" data-args="700, 700, true, null, 0" Text="View" ToolTip="View this tracking item" NavigateUrl='<%# Bind("ID","~/admin/email-templates/view-tracking.aspx?tId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="40px" CssClass="tac" />                               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ID") %>' CommandName="DeleteItem" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvEmailTemplate" HideOnEmpty="False"/>

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvEmailTemplate" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
