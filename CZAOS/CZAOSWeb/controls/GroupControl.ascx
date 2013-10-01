<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupControl.ascx.cs" Inherits="CZAOSWeb.controls.GroupControl" %>

<div class="group-container">
    <span class="pr5">Group</span> <asp:Literal runat="server" ID="litID"></asp:Literal>
    <mack:RequiredTextBox runat="server" ID="txtGroupName" MaxLength="20" ErrorMessage="&nbsp;" ValidatorCssClass="error" CssClass="ml10" ></mack:RequiredTextBox>
    
    <div class="group-checkboxlist">
        <asp:CheckBoxList runat="server" ID="cblAnimals" DataTextField="CommonName" DataValueField="AnimalID" CssClass="checkbox-list group-cbx" AutoPostBack="false" ></asp:CheckBoxList>
    </div>
    <span class="gc-count">0</span><span class="pl5">Animals Selected:</span><span class="pl5 fs10 i">(<asp:Literal runat="server" ID="litMax">0</asp:Literal> maximum)</span>
</div>
