using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KT.Extensions;

namespace CZAOSWeb.masterpages
{
    public partial class Teacher : System.Web.UI.MasterPage
    {
        public string ContentTitle
        {
            get
            {
                return litContentTitle.Text;
            }
            set
            {
                litContentTitle.Text = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (this.ContentTitle.IsNullOrEmpty())
            {
                this.ContentTitle = this.Page.Title;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginId"] == null)
                {
                    if (!this.Page.User.Identity.IsAuthenticated)
                    {
                        Response.Redirect("/default.aspx?ReturnUrl=" + this.Request.RawUrl);
                    }
                    else
                    {
                        Session["LoginId"] = this.Page.User.Identity.Name;
                    }

                }
                else
                {
                    Response.ClearHeaders();
                    Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                    Response.AddHeader("Pragma", "no-cache");
                }

            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}