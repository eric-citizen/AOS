using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CZAOSCore.Enums;
using System.Net.Mail;
using System.Net;
using CZAOSMail.Mail;

namespace CZAOSWeb.admin
{
    public partial class email_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!Roles.IsUserInRole(CoreUserTypeRoles.MasterAdmin.ToString()))                
                {
                    Response.Redirect("/admin/");
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (txtEmailFrom.IsValid && txtEmailTo.IsValid)
            {
                Notifications emailSender = new Notifications();
                bool result = emailSender.SendQuickMail(txtEmailTo.Text, "Test Email", "Web", txtEmailFrom.Text, "Test Message");

                if (result)
                {
                    divMessage.Text = "Send Success!";
                    divMessage.MessageType = KT.WebControls.MessageDiv.MessageTypes.success;
                }
                else
                {
                    divMessage.Text = "Send Fail: " + emailSender.LastError;
                    divMessage.MessageType = KT.WebControls.MessageDiv.MessageTypes.error;
                }
            }
            else
            {
                divMessage.Text = "Invalid email address";
                divMessage.MessageType = KT.WebControls.MessageDiv.MessageTypes.error;
            }
        }
    }
}