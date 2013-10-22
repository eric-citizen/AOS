<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridConfirmControl.ascx.cs" Inherits="CZAOSWeb.controls.GridConfirmControl" %>

<%--<asp:Button runat="server" ID="hyperValue" CssClass="delete" OnClick="deleteClick" ToolTip="Delete">    
</asp:Button>

<asp:Panel runat="server" ID="delRowItem" CssClass="hideDelete">
    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" 
        CommandName="DeleteItem" Text="" ToolTip="Confirm" CssClass="confirmDelete" 
        OnClick="lnkDelete_Click">
        </asp:LinkButton>
    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" 
        CommandName="DeleteItem" Text="" ToolTip="Cancel" CssClass="cancelDelete" > 
        </asp:LinkButton>
</asp:Panel>--%>

<div onclick="$(this).parent().children('div').toggle();" class="delete"></div>

<div style="display:none">
    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" 
        CommandName="DeleteItem" Text="" ToolTip="Confirm" CssClass="confirmDelete" 
        OnClick="lnkDelete_Click">
        </asp:LinkButton>
    <%--<asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" 
        Text="" ToolTip="Cancel" CssClass="cancelDelete" > 
        </asp:LinkButton>--%>
    <a href="javascript: void(0);" onclick="$(this).parent().parent().children('div').toggle();" title="Cancel" id="lnkCancel" class="cancelDelete"></a>
</div>