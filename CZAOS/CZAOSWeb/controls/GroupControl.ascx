<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupControl.ascx.cs" Inherits="CZAOSWeb.controls.GroupControl" %>

<div class="group-container">
    <span class="pr5">Group</span> <asp:Literal runat="server" ID="litID"></asp:Literal>
    <mack:RequiredTextBox runat="server" ID="txtGroupName" MaxLength="20" ErrorMessage="&nbsp;" ValidatorCssClass="error" CssClass="ml10" ></mack:RequiredTextBox>
    
    <div class="group-checkboxlist">
        <asp:ListBox runat="server" ID="lstAnimals" DataTextField="CommonName" SelectionMode="Multiple" Rows="10"
            DataValueField="AnimalID" CssClass="listbox" AutoPostBack="false" ></asp:ListBox>
    </div>
    <span class="gc-count">0</span><span class="pl5">Animals Selected:</span><span class="pl5 fs10 i">(<asp:Literal runat="server" ID="litMax">0</asp:Literal> maximum)</span>
</div>
