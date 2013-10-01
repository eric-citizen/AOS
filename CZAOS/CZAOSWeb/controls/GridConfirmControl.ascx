<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridConfirmControl.ascx.cs" Inherits="CZAOSWeb.controls.GridConfirmControl" %>

<asp:HyperLink runat="server" ID="hyperValue" CssClass="del-link" NavigateUrl="javascript:void(0);" ToolTip="Delete">    
    <asp:Image runat="server" ID="imgConfirm" ImageUrl="~/images/trash-icon16.png" Width="16px" Height="16px" />
</asp:HyperLink>

<div class="del-row-item" style="display:none;">
    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" 
        CommandName="DeleteItem" Text="" ToolTip="Confirm" CssClass="gcc-confirm" 
        OnClick="lnkDelete_Click">
            <img src="/images/16x16empty.png" style="width:16px; height:16px;" alt=""/>
        </asp:LinkButton>
    <a href="javascript:void(0);" class="del-cancel" title="Cancel">
        <img src="/images/16x16empty.png" style="" alt="" />
    </a>
</div>