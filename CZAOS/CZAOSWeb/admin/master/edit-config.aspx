<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-config.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_config" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />    

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit</asp:Literal> Config Value
        </legend>   
        
        <ul>
            <li class="required">
                <label>Key:</label>
                <mack:RequiredTextBox runat="server" ID="txtKey" CssClass="alphanumeric focusme" MaxLength="250" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a key" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Code:</label>
                <mack:RequiredTextBox runat="server" ID="txtValue" CssClass="alphanumeric" MaxLength="50" Width="200px" Required="true" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a value"></mack:RequiredTextBox>     
            </li>           
            
            <li class="tar pt15">
                <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" />
            </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
