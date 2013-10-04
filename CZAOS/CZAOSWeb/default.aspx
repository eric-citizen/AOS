<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Public.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CZAOSWeb._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

        <asp:MultiView runat="server" ID="mvLogin" ActiveViewIndex="0">
        <asp:View runat="server" ID="vwLogin">

            <fieldset class="form-fieldset" id="login">
                <legend>Administrative Login</legend>

                <ul>
                    <li class="required">
                        <label for="txtUsername">Username:</label>
                        <mack:RequiredTextBox runat="server" ID="txtUsername" CssClass="username" ErrorMessage="*" ValidatorToolTip="Username is required" ValidatorCssClass="error" SetFocusOnError="true" MaxLength="50"></mack:RequiredTextBox>
                    </li>
                    <li class="required">
                        <label for="txtPassword">Password:</label>
                        <mack:RequiredTextBox runat="server" ID="txtPassword" CssClass="password" TextMode="Password" ErrorMessage="*" ValidatorToolTip="Username is required" ValidatorCssClass="error" SetFocusOnError="true" MaxLength="50"></mack:RequiredTextBox>
                    </li>
                    <li>&nbsp;</li>
                    <li class="tar">
                        <mack:WaitButton runat="server" ID="btnSubmitLogin" Text="Login" CausesValidation="true" OnClick="btnSubmitLogin_Click" CssClass="button submit" />
                    </li>   
                    <li>
                        <mack:MessageDiv runat="server" ID="errorMessage" MessageType="error"></mack:MessageDiv>
                    </li>        
                </ul>

                <a href="dialogs/reminders.aspx" class="ui-dialog-link" data-args="400, 500, true, null, 1">Forgotten username or password?</a>

            </fieldset>            

        </asp:View>
        <asp:View runat="server" ID="vwReminders">

            <asp:MultiView runat="server" ID="mvPassword" ActiveViewIndex="0">
                <asp:View runat="server" ID="vwEmail">

                    <fieldset class="form-fieldset">
                        <legend>Forgotten Password?</legend>

                        <div class="password_message pr">

                            <label for="UserName">Username:</label>
                            <br />
                            <mack:RequiredTextBox ID="UserName" runat="server" Width="200px" Required="true" ErrorMessage="*" ValidatorToolTip="Enter your username"
                                ValidatorCssClass="error" ValidationGroup="password" CssClass="username" SetFocusOnError="true"></mack:RequiredTextBox>

                            <div style="position: absolute; right: 0; top: 10px;">
                                <mack:WaitButton runat="server" ID="btnSubmit" CommandName="Submit" CssClass="cms-button blue" Text="Send Password" ValidationGroup="password" OnClick="btnSubmit_Click" />
                            </div>

                        </div>

                    </fieldset>

                </asp:View>
                <asp:View runat="server" ID="vwSuccess">

                    <fieldset class="form-fieldset">
                        <legend>Password Sent</legend>
                        <div class="message info">
                            Your password has been sent to the email address on file.
                        </div>
                    </fieldset>

                </asp:View>
                <asp:View runat="server" ID="vwError">

                    <fieldset class="form-fieldset">
                        <legend>Password Lookup Error</legend>
                        <div class="message error">
                            There was an error retrieving your information. Please contact the
                            <asp:HyperLink runat="server" ID="lnkAdmin" Text="system administrator"></asp:HyperLink>.

                    <br />
                            <label>Error:</label><br />
                            <asp:Literal runat="server" ID="litError"></asp:Literal>

                        </div>
                    </fieldset>

                </asp:View>
            </asp:MultiView>

            <div class="h20">&nbsp;</div>

            <asp:MultiView runat="server" ID="mvUserName" ActiveViewIndex="0">
                <asp:View runat="server" ID="vwUserName">

                    <fieldset class="form-fieldset">
                        <legend>Forgotten Username?</legend>
                        <div class="password_message pr">

                            <label for="txtEmail">Email Address:</label>
                            <br />
                            <mack:EmailTextBox ID="txtEmail" runat="server" Width="200px" Required="true" ErrorMessage="*" ValidatorToolTip="Enter your email address"
                                ValidatorCssClass="error" ValidationGroup="username" SetFocusOnError="true"></mack:EmailTextBox>
                            <div style="position: absolute; right: 0; top: 10px;">
                                <mack:WaitButton runat="server" ID="btnSubmitEmail" CssClass="cms-button blue" Text="Send Username" ValidationGroup="username" OnClick="btnSubmitEmail_Click" />
                            </div>


                        </div>

                    </fieldset>

                </asp:View>
                <asp:View runat="server" ID="vwUserSuccess">

                    <fieldset class="form-fieldset">
                        <legend>Username Sent</legend>
                        <div class="message info">
                            Your username has been sent to the email address on file.
                        </div>
                    </fieldset>

                </asp:View>
                <asp:View runat="server" ID="vwUserError">

                    <fieldset class="form-fieldset">
                        <legend>Username Lookup Error</legend>
                        <div class="message error">
                            There was an error retrieving your information. Please contact the
                            <asp:HyperLink runat="server" ID="lnkAdmin2" Text="system administrator"></asp:HyperLink>.

                    <br />
                            <label>Error:</label><br />
                            <asp:Literal runat="server" ID="litEmailError"></asp:Literal>

                        </div>
                    </fieldset>

                </asp:View>
            </asp:MultiView>

        </asp:View>
    </asp:MultiView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="/assets/scripts/login.js"></script>
    <script>
        function checkForIframe() {
            var parentWin = window;
            while (parentWin != parentWin.parent)
                parentWin = parentWin.parent;

            if (parentWin != window)
                parentWin.location.href = window.location.href;
        }

        if (window.addEventListener) { // Mozilla, Netscape, Firefox
            window.addEventListener('load', checkForIframe, false);
        }
        else if (window.attachEvent) { // IE
            window.attachEvent('onload', checkForIframe);
        }
        else {
            window.onload = function () { checkForIframe(); }
        }
    </script>
</asp:Content>
