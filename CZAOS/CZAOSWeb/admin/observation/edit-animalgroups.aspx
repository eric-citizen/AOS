﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="edit-animalgroups.aspx.cs" Inherits="CZAOSWeb.admin.observation.edit_animalgroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:HiddenField runat="server" ID="hdnID" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">Edit Timed Info</asp:Literal>
        </legend>   

        <ul>
            <li class="required">
                <label>Start Time:</label>
                <mack:RequiredTextBox runat="server" ID="txtStartTime" ClientIDMode="Static" MaxLength="7" Required="true" Width="100px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the start time"></mack:RequiredTextBox>     
            </li>
            <li class="required">
                <label>End Time:</label>
                <mack:RequiredTextBox runat="server" ID="txtEndTime" ClientIDMode="Static" MaxLength="7" Required="true" Width="100px" 
                    ErrorMessage="*" ValidatorCssClass="error" ValidatorToolTip="Enter the end time" ></mack:RequiredTextBox>     
            </li>
            <li>
                <label>Interval:</label>
                <span id="interval"><asp:Literal runat="server" ID="litInterval"></asp:Literal></span><span class="pl10">minutes.</span>
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
