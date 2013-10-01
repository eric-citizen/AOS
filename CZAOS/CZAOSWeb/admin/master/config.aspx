<%@ Page Title="Manage System Configuration" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="config.aspx.cs" Inherits="CZAOSWeb.admin.master.config" %>
<%@ Register Src="~/controls/GridConfirmControl.ascx" TagPrefix="uc1" TagName="GridConfirmControl" %>
<%@ Register Src="~/admin/master/masterlinks.ascx" TagPrefix="uc1" TagName="masterlinks" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <uc1:masterlinks runat="server" id="masterlinks" />
    <a class="add-link ui-dialog-link" href="/admin/master/edit-config.aspx" data-args="250, 600, true, null, 1">Add New Config Value</a>

    <asp:GridView ID="gvConfig" runat="server" AllowSorting="False" AllowPaging="False" CssClass="gridview" AutoGenerateColumns="False" Width="100%" DataKeyNames="ID" PagerSettings-Visible="false" OnRowCommand="gvConfig_RowCommand" >
        <Columns>
            
            <asp:BoundField DataField="Description" HeaderText="Description">                
            </asp:BoundField> 
            <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-Width="200px">                
            </asp:BoundField> 
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="lnkEdit" CssClass="ui-dialog-link gv-edit-link" data-args="250, 600, true, null, 1" Text="Edit" ToolTip="Edit Config Setting" 
                        NavigateUrl='<%# Bind("ID","~/admin/master/edit-config.aspx?id={0}") %>'></asp:HyperLink>
                </ItemTemplate> 
                <ItemStyle Width="60px" CssClass="tac" />               
            </asp:TemplateField>           
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <uc1:GridConfirmControl runat="server" ID="GridConfirmControl" CommandArgument='<%#Bind("ID") %>' CommandName="DeleteConfig" />
                </ItemTemplate>                
                <ItemStyle Width="60px" CssClass="tac" />
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <mack:MessageDiv runat="server" ID="divEmpty" ListControlID="gvConfig" Text="No records found!"></mack:MessageDiv>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
