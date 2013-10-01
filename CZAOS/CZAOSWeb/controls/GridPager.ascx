<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridPager.ascx.cs" Inherits="CZAOSWeb.controls.GridPager" %>
<div id="litTotalDiv" runat="server" clientidmode="static">
    <asp:Literal runat="server" ID="litTotal"></asp:Literal>
</div>

<asp:Panel ID="pagerPanel" runat="server" CssClass="pagerPanel">
        
    <table>
        <tr>            
            <td><asp:ImageButton ID="previousImageButton" CssClass="page-arrow" runat="server" ImageUrl="~/images/prev.gif" AlternateText="Previous Page" ToolTip="Previous Page" CausesValidation="false" OnClick="previousImageButton_Click"/></td>
            <td style="padding:0 4px"><asp:Label id="pageLabel" runat="server">&nbsp;</asp:Label></td>
            <td><asp:ImageButton ID="nextImageButton" runat="server" CssClass="page-arrow" ImageUrl="~/images/next.gif" AlternateText="Next Page" ToolTip="Next Page" CausesValidation="false" OnClick="nextImageButton_Click" /></td>
            <td><asp:DropDownList ID="pageJumpDropDownList" runat="server" AutoPostBack="true" CausesValidation="false" style="padding-left:6px;" OnSelectedIndexChanged="pageJumpDropDownList_SelectedIndexChanged">
                    <asp:ListItem Selected="true" Text="Jump to" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
</asp:Panel>