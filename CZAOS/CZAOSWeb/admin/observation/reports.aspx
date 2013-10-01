<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="CZAOSWeb.admin.observation.observation_reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <h1>Upload component removed - no license</h1>
    <asp:HiddenField runat="server" ID="hdnID" />

    <div id="uploader" class="loading" style="height:250px;">
		&nbsp;
	</div>

    <div class="fs10" style="position: absolute; bottom:0;">Allowed file extensions: jpg, gif, docx, doc, xls, xlsx, pdf</div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    
</asp:Content>
