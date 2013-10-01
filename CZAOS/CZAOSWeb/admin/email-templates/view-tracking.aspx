<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="view-tracking.aspx.cs" Inherits="CZAOSWeb.admin.dialogs.view_tracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <mack:HiddenID runat="server" ID="hdnItemID" ClientIDMode="Static" Value="0" />
    
    <fieldset class="form-fieldset">

        <legend>
            <asp:Literal runat="server" ID="fieldsetLegend">View Email</asp:Literal>
        </legend>          

        <ul>
            <li>
                <label>Sent:</label>
                <asp:Literal runat="server" ID="litSent"></asp:Literal>
            </li>
            <li>
                <label>To:</label>
                <asp:Literal runat="server" ID="litTo"></asp:Literal>
                <br />
                <label>From:</label>
                <asp:Literal runat="server" ID="litFrom"></asp:Literal>
            </li>            
            <li>
                <label>Sent By:</label>
                <asp:Literal runat="server" ID="litUser"></asp:Literal>   
            </li>
            <li>
                <label>Send OK:</label>
                <div style="max-height: 300px; overflow:auto;">
                <asp:Literal runat="server" ID="litSendOK"></asp:Literal>  
                    </div> 
            </li>
            <li>
                <label>Opened:</label>
                <asp:Literal runat="server" ID="litOpened"></asp:Literal>   
            </li>
            <li>
                <label>Subject:</label>
                <asp:Literal runat="server" ID="litSubject"></asp:Literal>   
            </li>
            <li>
                <label>Email Body:</label>
                
            </li>     
           <li>
               <div style="height: 300px; overflow:auto;">
                    <asp:Literal runat="server" ID="litBody"></asp:Literal>
                </div>
           </li>
        </ul>
        
</fieldset>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">    
</asp:Content>
