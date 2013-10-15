﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNav.ascx.cs" Inherits="CZAOSWeb.controls.AdminNav" %>
<div class="menu_drop topnav">
<ul id="admin-nav-list" >
    <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_ItemDataBound" OnPreRender="rptMenu_PreRender">
        <HeaderTemplate>
            <li>
                <a href="/admin/">Home</a>
            </li>
        </HeaderTemplate>
        <ItemTemplate >
            <li>
                <asp:HyperLink runat="server" ID="lnkNav" Text='<%# Bind("NavText") %>' NavigateUrl='<%# Bind("Folder","~/admin/{0}") %>'></asp:HyperLink>
            </li>
        </ItemTemplate>
        
    </asp:Repeater>
</ul>
</div>
<%--<ul class="menu_drop topnav">
    <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_ItemDataBound" OnPreRender="rptMenu_PreRender">
        <ItemTemplate >
            <li>
                <asp:HyperLink runat="server" ID="lnkNav" Text='<%# Bind("NavText") %>' NavigateUrl='<%# Bind("Folder","~/admin/{0}") %>'></asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>--%>

<%--<div class="menu_drop topnav">
    <ul>
        <li><a href="/admin/observation/">Observations</a></li>
        <li><a href="/admin/animals/">Animals</a></li>
        <li><a href="/admin/exhibits/">Exhibits</a></li> 
        <li><a href="/admin/exhibits/index.aspx">Exhibit Locations</a></li>
        <li><a href="/admin/behavior/">Behavior Categories</a></li>
        <li><a href="/admin/weather/">Weather</a></li>
        <li><a href="/admin/schools/">Schools</a></li>
        <li><a href="/admin/changes/">Change Log</a></li>
    </ul>
</div>--%>