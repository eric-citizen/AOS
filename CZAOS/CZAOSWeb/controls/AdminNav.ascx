<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNav.ascx.cs" Inherits="CZAOSWeb.controls.AdminNav" %>
<ul id="admin-nav-list">
    <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_ItemDataBound" OnPreRender="rptMenu_PreRender">
        <HeaderTemplate>
            <li>
                <a href="/admin/">Home</a>
            </li>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <asp:HyperLink runat="server" ID="lnkNav" Text='<%# Bind("NavText") %>' NavigateUrl='<%# Bind("Folder","~/admin/{0}") %>'></asp:HyperLink>
            </li>
        </ItemTemplate>
        
    </asp:Repeater>
</ul>