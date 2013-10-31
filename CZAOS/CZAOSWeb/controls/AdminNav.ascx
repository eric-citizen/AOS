<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNav.ascx.cs" Inherits="CZAOSWeb.controls.AdminNav" %>
    <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_ItemDataBound" OnPreRender="rptMenu_PreRender">
        <HeaderTemplate>
            <li>
                <a class="ui-dialog-link" href="/admin/edit-profile.aspx" data-args="450, 750, true, null, 0" title="Edit Profile">Edit Profile</a>
            </li>
        </HeaderTemplate>
        <ItemTemplate >
            <li>
                <asp:HyperLink runat="server" ID="lnkNav" Text='<%# Bind("NavText") %>' NavigateUrl='<%# Bind("Folder","~/admin/{0}") %>'></asp:HyperLink>
            </li>
        </ItemTemplate>
        
    </asp:Repeater>