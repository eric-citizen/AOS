<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditableTextBox.ascx.cs" Inherits="CZAOS.controls.EditableTextBox" %>

<asp:HyperLink runat="server" ID="hyperValue" CssClass="etb-link" NavigateUrl="javascript:void(0);"></asp:HyperLink>
<div class="etb-editable" style="display:none;" id="editbox" runat="server">
     <asp:TextBox runat="server" ID="txtWrite" Width="70%" CssClass="etb-box"></asp:TextBox>
      
     <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update" CssClass="etb-update" 
         CausesValidation="true" style="margin-right: 3px;" onclick="lnkUpdate_Click"></asp:LinkButton>
     
     <a href="javascript:void(0);" class="etb-cancel">Cancel</a>
          
     <asp:RequiredFieldValidator runat="server" ID="rfvWrite" ErrorMessage="Please enter a value" CssClass="error" 
        Display="Dynamic" ControlToValidate="txtWrite"></asp:RequiredFieldValidator>
</div>