<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb.admin.users._default" %>

<%@ Register Src="~/controls/UserGrid.ascx" TagPrefix="uc1" TagName="UserGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <a href="edit-user.aspx" class="ui-dialog-link" data-args="700, 800, true, null, 1" title="Add New User">Add New User</a>

    <!-- jquery tabs ui //-->
    <div id="tabs">
        <ul>
            <li><asp:HyperLink runat="server" ID="lnkAdmin" NavigateUrl="#tab1">Administrators</asp:HyperLink></li>            
            <li><asp:HyperLink runat="server" ID="lnkTeachers" NavigateUrl="#tab2">Education Admin</asp:HyperLink></li>
            <li><asp:HyperLink runat="server" ID="lnkStudents" NavigateUrl="#tab3">Observer</asp:HyperLink></li>      
            <li><asp:HyperLink runat="server" ID="lnkMaster" NavigateUrl="#tab4">Master Admin</asp:HyperLink></li>           
        </ul>     
    
        <div class="tab" id="tab1">            
            <uc1:UserGrid runat="server" id="UserGrid" UserType="Administrator" />
        </div>
        <div id="tab2">            
             <uc1:UserGrid runat="server" id="UserGrid3" UserType="EducationAdmin" />
        </div>
        <div id="tab3">            
            <uc1:UserGrid runat="server" id="UserGrid2" UserType="Observer" />
        </div>
        <div id="tab4">            
            <uc1:UserGrid runat="server" id="UserGrid1" UserType="MasterAdmin" />
        </div>
        
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">



</asp:Content>
