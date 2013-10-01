<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-wind.aspx.cs" Inherits="CZAOSWeb.admin.weather.edit_wind" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Wind Condition</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>Condition:</label>
                <mack:RequiredTextBox runat="server" ID="txtCondition" CssClass="alphanumeric focusme" MaxLength="100" Width="200px" Required="true" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter a condition" ></mack:RequiredTextBox>     
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
