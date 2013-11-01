using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CZAOSCore.basepages;
using System.Web.Security;
using KT.Extensions;
using CZAOSCore;
using CZAOSCore.Logging;
using CZAOSMail.Mail;
using System.Net.Mail;

namespace CZAOSWeb
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (this.User.Identity.IsAuthenticated)
                {
                    //Response.Redirect("/admin/");
                    //return;
                }

                if (this.Request.QueryString.Contains(KT.WebControls.AutoLogout.KEY) && (bool)this.Request.QueryString.Contains(KT.WebControls.AutoLogout.KEY))
                {                    

                    PageMessageOptions options = new PageMessageOptions();
                    options.OpaqueBg = true;
                    options.FadeOut = false;

                    string s = "For security purposes your session was auto-logged out at {0} due to inactivity.<p>Please login again to continue.</p>".FormatWith(System.DateTime.Now.ToLongTimeString());
                    this.Page.DisplayMessage(s, options);

                    Session.Abandon();
                    FormsAuthentication.SignOut();

                }

                //CZAOSMail.Mail.Notifications mailer = new Notifications();
                // EmailMessage mail = new EmailMessage();

                //    //mail.To.Add(new MailAddress(user.EmailAddress, user.DisplayName));
                //    mail.To.Add(new MailAddress("ken@kentilley.com", "Ken Tilley"));
                //    mail.Subject = "Email Tracking Test";
                //    mail.Body = "<b>this is a test2</b>";

                ////"ktilley@thirtyonegifts.com"
                //mailer.SendGoogleMail(mail);//   ("ktilley@thirtyonegifts.com", "Email Tracking Test", "kenscotttilley", "kenscotttilley@gmail.com", "<b>this is a test2</b>");


            }
        }

        protected void btnSubmitLogin_Click(object sender, EventArgs e)
        {
            //this.AttemptCount += 1;

            //if (!this.CheckAttempts())
            //{
            //    return;
            //}


            if (txtUsername.IsValid && txtPassword.IsValid)
            {
                string strUsername = txtUsername.HtmlEncodedText();
                string strPassword = txtPassword.HtmlEncodedText();

                if (this.ValidateLogin(strUsername, strPassword))
                {
                    FormsAuthentication.Initialize();
                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(strUsername, false);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, "");

                    authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                    Response.Cookies.Add(authCookie);
                    Session["LoginId"] = strUsername;

                    Logger.Log(LogTarget.Security, MessageLevel.Info, "User {0} logged in to the admin module successfully.".FormatWith(strUsername));

                    string url = FormsAuthentication.GetRedirectUrl(strUsername, false);
                    FormsAuthentication.RedirectFromLoginPage(strUsername, false);

                    //Response.Write(FormsAuthentication.GetRedirectUrl(strUsername, false));
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "btnSubmitLogin_Click", "top.window.location.href= '{0}';".FormatWith(FormsAuthentication.GetRedirectUrl(strUsername, false)), true);

                }
                else
                {
                    Logger.Log(LogTarget.Security, MessageLevel.Warning, "User {0} failed to log in to the admin module.".FormatWith(strUsername));
                    this.LoginError();
                }

            }
        }

        private bool ValidateLogin(string userName, string password)
        {
            bool membershipOk =  Membership.ValidateUser(userName, password);

            if (membershipOk)
            {
                CZDataObjects.CZUser user = CZBizObjects.UserList.GetUser(userName);
                if (user != null && user.ExpirationDate.HasValue)
                {
                    DateTime exp = Convert.ToDateTime(user.ExpirationDate);
                    userIsExpired = !exp.IsAfterDate(System.DateTime.Now);
                    return !userIsExpired;
                }

                return membershipOk;

            }
            else
            {
                return false;
            }
        }

        bool userIsExpired = false;

        private void LoginError()
        {
            MembershipUser user = Membership.GetUser(txtUsername.Text);

            if (this.IsDebug)
            {

                if ((user == null))
                {
                    this.ShowMessage("UsernameNotFound");

                }
                else if (user.IsLockedOut)
                {
                    this.ShowMessage(string.Format("user.IsLockedOut", AppSettings.GetSetting("AdminReplyAddress")));
                    //user.UnlockUser();

                }
                else if (!user.IsApproved)
                {
                    this.ShowMessage("Account exists but is not approved!");
                    user.IsApproved = true;
                    Membership.UpdateUser(user);

                }
                else if (userIsExpired)
                {
                    this.ShowMessage("User password is expired!");
                }
                else
                {
                    string s = user.GetPassword();
                    this.ShowMessage("InvalidPassword");
                }

            }
            else
            {
                if ((user == null))
                {
                    this.ShowMessage("Invalid Username/Password");

                }
                else
                {
                    if (user.IsLockedOut)
                    {
                        user.Comment += System.Environment.NewLine + string.Format("Login attempt while locked out at {0} from IP {1}", System.DateTime.Now.ToString("MM/dd/yy hh:mm tt"), this.CurrentUserIP);
                        Membership.UpdateUser(user);

                        this.ShowMessage(string.Format("AccountLockedOut", AppSettings.GetSetting("AdminReplyAddress")));
                        btnSubmitLogin.Visible = false;

                    }
                    else if (userIsExpired)
                    {
                        this.ShowMessage("User password is expired!");
                    }
                    else
                    {
                        this.ShowMessage("UnsuccessfulLogin");
                    }

                }

            }

        }

        private void ShowMessage(string message, PageMessageOptions.MessageType type = PageMessageOptions.MessageType.Error)
        {
            PageMessageOptions options = new PageMessageOptions();
            options.OpaqueBg = true;
            options.FadeOut = false;
            options.IconType = type;

            this.Page.DisplayMessage(message);
        }

        protected void lnkReminders_Click(object sender, EventArgs e)
        {
            mvLogin.ActiveViewIndex = 1;
            UserName.Clear();

            lnkAdmin.NavigateUrl = "mailto:" + AppSettings.GetSetting<string>("AdminReplyAddress");
            lnkAdmin2.NavigateUrl = lnkAdmin.NavigateUrl;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (UserName.IsValid)
            {
                Notifications mailer = new Notifications();
                if (mailer.SendUserCredentials(UserName.Text, this))
                {
                    mvPassword.ActiveViewIndex = 1;
                }
                else
                {
                    mvPassword.ActiveViewIndex = 2;
                    litError.Text = mailer.LastError;
                }

                mvUserName.Visible = false;
            }

        }

        protected void btnSubmitEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.IsValid)
            {
                string userName = Membership.GetUserNameByEmail(txtEmail.Text);

                if (userName.IsNullOrEmpty())
                {
                    mvUserName.ActiveViewIndex = 2;
                    litEmailError.Text = "Username not found.";
                }
                else
                {
                    Notifications mailer = new Notifications();
                    if (mailer.SendUserCredentials(userName, this))
                    {
                        mvUserName.ActiveViewIndex = 1;
                    }
                    else
                    {
                        mvUserName.ActiveViewIndex = 2;
                        litEmailError.Text = mailer.LastError;
                    }
                }

                mvPassword.Visible = false;
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cookies.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }        
    }
}