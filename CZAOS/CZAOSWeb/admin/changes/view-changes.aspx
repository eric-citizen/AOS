<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="view-changes.aspx.cs" Inherits="CZAOSWeb.admin.changes.view_changes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" />
    <mack:HiddenID runat="server" ID="hdnObjectName" />

    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">View Change Log</asp:Literal>
        </legend>   

        <ul>
            <li>
                <label>Table:</label>
                <asp:Literal runat="server" ID="litTable"></asp:Literal>
            </li>            
            <li>
                <label>Editor:</label>
                <asp:Literal runat="server" ID="litName"></asp:Literal>
            </li>
            <li>
                <label>Date:</label>
                <asp:Literal runat="server" ID="litDate"></asp:Literal>
            </li>
            <li>
                <label>Type:</label>
                <asp:Literal runat="server" ID="litType"></asp:Literal>
            </li>
            <li>
                <label>Changes:</label>
                <asp:GridView runat="server" ID="gvChanges" Width="100%" CssClass="gridview" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="FieldName" HeaderText="Field" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Old" HeaderText="Old Value"  ItemStyle-Width="40%"/>
                        <asp:BoundField DataField="New" HeaderText="New Value" ItemStyle-Width="40%" />
                    </Columns>
                </asp:GridView>
                
            </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
