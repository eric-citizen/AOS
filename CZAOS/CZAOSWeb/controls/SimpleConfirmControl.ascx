<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimpleConfirmControl.ascx.cs" Inherits="CZAOSWeb.controls.SimpleConfirmControl" %>

<asp:HyperLink runat="server" ID="lnkConfirm" CssClass="ui-scc-confirm" Text="[CONFIRM LINK TEXT]" NavigateUrl="javascript:void(0);"></asp:HyperLink>

<div id="<%= this.DataControlId %>" class="scc-box">

    <div class="scc-title">
        <asp:Literal runat="server" ID="litTitle"></asp:Literal>        
    </div>      
                   
    <div class="scc-content">                    
        <asp:Literal runat="server" ID="litContent"></asp:Literal> 
    </div>
    
    <div class="scc-buttons">
        <mack:WaitButton runat="server" ID="btnOK" Text="OK" CausesValidation="false" OnClick="btnOK_Click" Width="120px" CssClass="button" />        
        <input type="button" id="cancelConfirmButton" causesvalidation="false" title="Cancel" value="Cancel" class="ui-dialog-close button" />                    
    </div>
    
</div>