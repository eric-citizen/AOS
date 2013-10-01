<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-exhibit-location.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.edit_exhibit_location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />
    <mack:HiddenID runat="server" ID="hdnExhibitID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Exhibit Location</asp:Literal>
        </legend>   

        <ul>
            <li>
                <label>Exhibit:</label>
                <asp:Literal runat="server" ID="litName"></asp:Literal>
            </li>
            <li class="required">
                <label>Location:</label>
                 <mack:RequiredDropDownList runat="server" ID="ddlLocation" Required="true" DataTextField="Description" DataValueField="LocationID" 
                     SetFocusOnError="true" ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="select a location" InitialValue="-1"  ></mack:RequiredDropDownList>
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
