<%@ Page Title="View Change Log" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.changes._default" %>
<%@ Register Src="~/controls/GridPager.ascx" TagPrefix="uc1" TagName="GridPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">    

    <div class="pr">
        <label class="required">Free Text Search:</label>
        <span style="display:inline-block; width:220px;">
            <mack:RequiredTextBox runat="server" ID="txtFreeText" MaxLength="50" Width="150px" ValidationGroup="freetext" ValidatorCssClass="error" ValidatorToolTip="Please enter search text"></mack:RequiredTextBox>
        </span>
        <mack:WaitButton runat="server" ID="btnSearch" Text="Search" CssClass="button" ValidationGroup="freetext" OnClick="btnSearch_Click"/>
        <asp:LinkButton runat="server" ID="lnkClear" Text="Clear" OnClick="lnkClear_Click"></asp:LinkButton>
    </div>

    <asp:ObjectDataSource ID="cztDataSource" runat="server" OnSelected="cztDataSource_Selected"
        SelectMethod="GetItemCollection" TypeName="CZBizObjects.ChangeLogList" OnSelecting="cztDataSource_Selecting"
        EnablePaging="True" SortParameterName="sortExpression" SelectCountMethod="GetCount" OldValuesParameterFormatString="original_{0}">
       
        <SelectParameters>        
            <asp:Parameter Name="filterExpression" type="string" DefaultValue="" />
        </SelectParameters>
        
    </asp:ObjectDataSource>

    <mack:GridViewSortExtender runat="server" ID="gvse"
        AscendingImageUrl="~/images/down.png" DescendingImageUrl="~/images/up.png" GridViewID="gvLog" TransparentImageUrl="~/images/transparent.png" />

    <asp:GridView ID="gvLog" runat="server" DataSourceID="cztDataSource" AllowSorting="True" AllowPaging="True" CssClass="gridview"
        PageSize="20" AutoGenerateColumns="False" Width="100%" PagerSettings-Visible="false" 
        DataKeyNames="ID" >
        <Columns>

            <asp:BoundField DataField="ChangeDate" DataFormatString="{0:ddd, MMM dd yyy hh:mm tt}" HtmlEncode="false" SortExpression="ChangeDate" HeaderText="Date" ItemStyle-Width="200px">                
            </asp:BoundField>   
            <asp:BoundField DataField="Identifier" SortExpression="Identifier" HeaderText="Object/ID" ItemStyle-Width="330px">                
            </asp:BoundField>
                    <asp:BoundField DataField="ChangeType" SortExpression="ChangeType" HeaderText="Type" ItemStyle-Width="80px">                
            </asp:BoundField> 
            <asp:BoundField DataField="UserDisplayName" SortExpression="UserDisplayName" HeaderText="User">                
            </asp:BoundField> 
            
                         

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkView" CssClass="ui-dialog-link gv-view-link" data-args="600, 700, true, null, 0" Text="View" ToolTip="View Changes" NavigateUrl='<%# Bind("ID","~/admin/changes/view-changes.aspx?id={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <uc1:GridPager runat="server" ID="gvPagerControl" GridViewID="gvLog" />

    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvLog" Text="No records found!"></mack:MessageDiv>

     

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
