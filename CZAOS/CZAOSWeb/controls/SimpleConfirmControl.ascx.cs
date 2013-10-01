using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;

namespace CZAOSWeb.controls
{
    public partial class SimpleConfirmControl : System.Web.UI.UserControl
    {

        //public delegate void Click(object source, EventArgs e);
        public event EventHandler Confirm;

        public string LinkText
        {
            get { return lnkConfirm.Text; }
            set { lnkConfirm.Text = value; }
        }

        public string LinkCssClass
        {
            get { return lnkConfirm.CssClass; }
            set { lnkConfirm.CssClass += " " + value; }
        }

        public string Title
        {
            get { return litTitle.Text; }
            set { litTitle.Text = value; lnkConfirm.ToolTip = value; }
        }

        public string Content
        {
            get { return litContent.Text; }
            set { litContent.Text = value; }
        }

        public string DataControlId
        {
            get
            {
                if (ViewState["DataControlId"] == null)
                {
                    ViewState["DataControlId"] = "scc-box";
                }

                return Convert.ToString(ViewState["DataControlId"]);

            }
            set
            {
                
                if(value.IsNotNullOrEmpty())
                    ViewState["DataControlId"] = value;

            }
        }
        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik, @toddanglin
        //Facebook: facebook.com/telerik
        //=======================================================


        protected void Page_Load(object sender, EventArgs e)
        {
            lnkConfirm.Attributes.Add("data-control-id", this.DataControlId);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (Confirm != null)
            {
                Confirm(this, e);
            }
        }
    }
}