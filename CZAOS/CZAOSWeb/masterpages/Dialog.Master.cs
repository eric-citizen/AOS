using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CZAOSWeb.masterpages
{
    public partial class Dialog : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RefreshParent()
        {
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "CZAOSWeb_masterpages_Dialog"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CZAOSWeb_masterpages_Dialog", "refreshParentDelay();", true);
            }

        }

        public void CloseUIModal()
        {
            //this.DisplayError("No observation found - missing id", "function:CZAOSUIDialogs.dialogCloseUIDialog();");
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "CZAOSWeb_masterpages_CloseDialog"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CZAOSWeb_masterpages_CloseDialog", "CZAOSUIDialogs.dialogCloseUIDialog();", true);
            }

        }   
    }
}