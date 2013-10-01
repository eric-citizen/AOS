<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-crowd.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_crowd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Crowd</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>Crowd:</label>
                <mack:RequiredTextBox runat="server" ID="txtCrowd" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a crowd" ></mack:RequiredTextBox>     
            </li>            
            <li>
                <label>Active:</label>   
                <asp:CheckBox runat="server" ID="chkActive" Checked="true" />  
            </li>
            
            <li class="tar pt15">
                <mack:WaitButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="Save" />
            </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
