using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using CZAOSCore.basepages;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;

namespace CZAOSWeb.admin
{
    public partial class _default : MainBase
    {
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ExtendSession()
        {

            System.DateTime now = System.DateTime.Now;
            HttpContext.Current.Session.Add("SessionUpdate", now.ToLongTimeString());
            return new JavaScriptSerializer().Serialize("success");           

        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik, @toddanglin
        //Facebook: facebook.com/telerik
        //=======================================================


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                bool show = Session.Get<bool>("nopermissions");

                if (show)
                {
                    this.DisplayError("You do not have permissions rights for the requested area.");
                    Session.Remove("nopermissions");
                }

                CZUser user = CZBizObjects.UserList.GetUser(this.User.Identity.Name);
                litName.Text = user.DisplayName;


            }

        }
    }
}