<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlphabetFilter.ascx.cs" Inherits="CZAOSWeb.controls.AlphabetFilter" %>

<asp:Label ID="captionLabel" runat="server" Text="Filter:" Font-Bold="true" ></asp:Label>&nbsp;
<asp:Panel runat="server" ID="repeaterDivPanel">
    <asp:Repeater ID="findRepeater" runat="server" OnItemDataBound="findRepeater_ItemDataBound" OnItemCommand="findRepeater_ItemCommand">
        <ItemTemplate>
            <asp:LinkButton runat="server" ID="nameLink" CssClass="alphabet-letter" Text='<%# Container.DataItem %>' CommandArgument='<%# Container.DataItem %>' CausesValidation="false"></asp:LinkButton> 
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
