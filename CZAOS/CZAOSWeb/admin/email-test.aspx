<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email-test.aspx.cs" Inherits="CZAOSWeb.admin.email_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="/assets/css/reset.css" rel="stylesheet" />
    <link href="/assets/css/messaging.css" rel="stylesheet" />
    <link href="/assets/css/helpers.css" rel="stylesheet" />
    <link href="/assets/css/czaos.css" rel="stylesheet" />
    <style>
        table
        {
            margin: 50px auto;

        }
        table td
        {
            padding: 3px;
        }
    </style>

    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js" type="text/javascript"></script> 

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 600px; line-height: 24px;">        
        <tr>
            <td style="width: 150px">SMTP Server:</td>
            <td>
                <asp:TextBox ID="serverTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr><td>SMTP Username:</td><td>
            <asp:TextBox ID="userTextBox" runat="server"></asp:TextBox></td></tr>
        <tr><td>SMTP Password:</td><td>
            <asp:TextBox ID="pwdTextBox" runat="server"></asp:TextBox></td></tr>
        <tr><td>To:</td><td>
            <mack:EmailTextBox runat="server" ID="txtEmailTo" ErrorMessage="Enter To email address"    Required="true" SetFocusOnError="true" ></mack:EmailTextBox>
        </td></tr>
        <tr><td>From:</td><td>
            <mack:EmailTextBox runat="server" ID="txtEmailFrom" ErrorMessage="Enter From email address"    Required="true" SetFocusOnError="true" ></mack:EmailTextBox>      
        </td></tr>
        
        <tr>
            <td class="vat">Delivery Method:</td>
            <td>
                <asp:RadioButtonList ID="rdoDeliveryMethod" runat="server">
                    <asp:ListItem>Network</asp:ListItem>
                    <asp:ListItem>SpecifiedPickupDirectory</asp:ListItem>
                    <asp:ListItem Selected="True">PickupDirectoryFromIis</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        
        <tr>
            <td colspan="2" class="tar">
                <mack:WaitButton runat="server" ID="btnSend" Text="Send" OnClick="btnSend_Click" />
             </td>
            </tr>            
            <tr>
                <td colspan="2">
                                   
                </td>
            </tr>       
            <tr>
                <td colspan="2">
                     <mack:MessageDiv runat="server" ID="divMessage" MessageType="info" ></mack:MessageDiv>
                </td>
            </tr>          
            
    </table>
    </div>
    </form>
</body>
</html>
