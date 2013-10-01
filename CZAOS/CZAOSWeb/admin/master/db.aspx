<%@ Page Title="Manage Database" Language="C#" MasterPageFile="~/masterpages/Main.Master" AutoEventWireup="true" CodeBehind="db.aspx.cs" Inherits="CZAOSWeb.admin.master.db" %>

<%@ Register Src="~/admin/master/masterlinks.ascx" TagPrefix="uc1" TagName="masterlinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <uc1:masterlinks runat="server" id="masterlinks" />

    <fieldset title="Enter Query" class="floatCenter p10" style="width: 740px; padding:10px;">
    
        <legend>Query:</legend>
        <mack:RequiredTextBox runat="server" ID="txtSQL" Width="100%" TextMode="MultiLine" Rows="8" Required="true" ErrorMessage="Please enter a valid sql query" ValidatorCssClass="required"></mack:RequiredTextBox>
        <mack:WaitButton runat="server" ID="btnExecute" CssClass="button floatRight mt10" Text="Execute" OnClick="btnExecute_Click" />
        
        <div class="clear">&nbsp;</div>

        <mack:WaitButton runat="server" ID="btnBackUp" CssClass="button mt10" Text="Backup DB" OnClick="btnBackUp_Click" CausesValidation="false" />
        <br />
        <asp:DropDownList runat="server" ID="ddlDB" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
        <mack:WaitButton runat="server" ID="btnRestore" CssClass="button mt10" Text="Restore DB" OnClick="btnRestore_Click" CausesValidation="false" />

    </fieldset>
    
    <fieldset class="p10" style="width: 100%;">
        <legend>Results:</legend>

        <asp:GridView runat="server" ID="gvResultSet" Width="720px" AutoGenerateColumns="true" SkinID="AdminGridSkin" CssClass="gridview">
            <EmptyDataTemplate>
                <div class="static_message">No Results</div>
            </EmptyDataTemplate>
        </asp:GridView>

        <ul>
            <li>
                <mack:WaitButton runat="server" ID="btnMenu" Text="Init Nav"  />
            </li>
        </ul>
    </fieldset>            
    
    <mack:MessageDiv runat="server" ID="msgError" MessageType="error" ></mack:MessageDiv>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
