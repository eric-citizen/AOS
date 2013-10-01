<%@ Page Title="Administration Dashboard" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>Welcome back <asp:Literal runat="server" ID="litName"></asp:Literal></h2>
    <br />
    <a href="edit-profile.aspx" class="ui-dialog-link" title="Edit your profile" data-width="800" data-height="600">Edit your profile.</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
