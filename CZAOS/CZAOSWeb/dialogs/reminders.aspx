<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/Dialog.Master" AutoEventWireup="true" CodeBehind="reminders.aspx.cs" Inherits="CZAOSWeb.dialogs.reminders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

            <asp:MultiView runat="server" ID="mvPassword" ActiveViewIndex="0">
                <asp:View runat="server" ID="vwEmail">

                    <fieldset class="form-fieldset">
                        <legend>Forgotten Password?</legend>

                        <div class="password_message pr">

                            <label for="UserName">Username:</label>
                            <br />
                            <mack:RequiredTextBox ID="UserName" runat="server" Required="true" ErrorMessage="*" ValidatorToolTip="Enter your username" Width="200px"
                                ValidatorCssClass="error" ValidationGroup="password" CssClass="username focusme" SetFocusOnError="true"></mack:RequiredTextBox>

                            <div style="position: absolute; right: 0; top: 15px;">
                                <mack:WaitButton runat="server" ID="btnSubmit" CommandName="Submit" CssClass="button" Text="Send Password" ValidationGroup="password" OnClick="btnSubmit_Click" />
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
                            <mack:EmailTextBox ID="txtEmail" runat="server" Required="true" ErrorMessage="*" ValidatorToolTip="Enter your email address" Width="200px"
                                ValidatorCssClass="error" ValidationGroup="username" SetFocusOnError="true"></mack:EmailTextBox>
                            <div style="position: absolute; right: 0; top: 15px;">
                                <mack:WaitButton runat="server" ID="btnSubmitEmail" CssClass="button" Text="Send Username" ValidationGroup="username" OnClick="btnSubmitEmail_Click" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    
</asp:Content>
