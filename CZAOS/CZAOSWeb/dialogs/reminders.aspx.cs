using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KT.Extensions;
using CZAOSCore;
using CZAOSCore.basepages;
using CZAOSMail.Mail;

namespace CZAOSWeb.dialogs
{
    public partial class reminders : MainBase 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}