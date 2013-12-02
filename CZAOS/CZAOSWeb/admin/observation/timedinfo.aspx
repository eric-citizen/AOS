<%@ Page Title="Manage Timed Info" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="timedinfo.aspx.cs" Inherits="CZAOSWeb.admin.observation.timedinfo" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <a class="add-link ui-dialog-link" href="/admin/observation/edit-timedinfo.aspx" title="Add New Timed Info" data-args="340, 500, true, null, 1">Add New Timed Info</a>   

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected" OnSelecting="cztDataSource_Selecting"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.TimedInfoList"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvObs" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvObs" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="TimedInfoID" OnRowCommand="gvObs_RowCommand" OnRowDataBound="gvObs_RowDataBound">
        <Columns>

            <asp:BoundField DataField="TimeStart" SortExpression="TimeStart" HeaderText="Start" DataFormatString="{0:hh:mmtt}">                
            </asp:BoundField>            
            <asp:BoundField DataField="TimeEnd" SortExpression="TimeEnd" HeaderText="End" DataFormatString="{0:hh:mmtt}">                
            </asp:BoundField> 
            <asp:BoundField DataField="Interval" SortExpression="Interval" HeaderText="Interval" ItemStyle-Width="50px">                
            </asp:BoundField>  
            <asp:TemplateField SortExpression="Active" HeaderText="Active" ItemStyle-Width="50px" HeaderStyle-CssClass="tac" ItemStyle-CssClass="tac cell-wait-click">                    
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="activeCheckBox" Checked='<%# Bind("Active") %>' OnCheckedChanged="IsActiveCheckChanged" AutoPostBack="true" />
                </ItemTemplate> 
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="340, 500, true, null, 1" Text="Edit" ToolTip="Edit this item" NavigateUrl='<%# Bind("TimedInfoID","~/admin/observation/edit-timedinfo.aspx?timedinfoId={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("TimedInfoID") %>' CommandName="DeleteTimedInfo" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>

        </Columns>

        
    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvObs" HideOnEmpty="False"/>

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvObs" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
