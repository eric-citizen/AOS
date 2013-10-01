<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-nav.aspx.cs" Inherits="CZAOSWeb.admin.nav.edit_nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit</asp:Literal> Nav Item
        </legend>   

        <ul>
            <li class="required">
                <label>Folder:</label>
                <mack:RequiredTextBox runat="server" ID="txtFolder" CssClass="alphanumeric focusme checkfolderexists" MaxLength="250" Width="300px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a folder name" ></mack:RequiredTextBox>     
            </li>    
            <li class="required">
                <label>Nav Text:</label>
                <mack:RequiredTextBox runat="server" ID="txtNavText" CssClass="alphanumeric focusme" MaxLength="250" Width="300px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter nav link text" ></mack:RequiredTextBox>     
            </li>          
            <li>
                <label>Roles:</label>   
                 <asp:CheckBoxList runat="server" ID="cbxRoles" CssClass="checkbox-list">
                     <asp:ListItem Text="Pro Admin" Value="Administrator"></asp:ListItem>
                     <asp:ListItem Text="Education Admin" Value="EducationAdmin"></asp:ListItem>
                 </asp:CheckBoxList>
            </li>
            
            <li class="tar pt15">
                <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" />
            </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

    <script>
        $(".checkfolderexists").blur(function () {
            var key = $(this).val();
            FolderExists($(this).attr("Id"), $(this).val());
        });
    </script>

</asp:Content>
