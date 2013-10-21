using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using KT.WebControls;

namespace CZAOSWeb.controls
{
    public partial class GridConfirmControl : System.Web.UI.UserControl
    {
        public event EventHandler Delete;        

        private void OnDelete(string arg)
        {
            RowCommandEventHandler args = new RowCommandEventHandler(arg, this.CommandName);
            if (Delete != null)
            {
                Delete(this, args);
            }

        }

        public string CommandArgument
        {
            get
            {
                if (ViewState["CommandArgument"] == null)
                {
                    ViewState["CommandArgument"] = string.Empty;
                }

                return Convert.ToString(ViewState["CommandArgument"]);
            }
            set
            {
                ViewState["CommandArgument"] = value;
                lnkDelete.CommandArgument = value;
            }
        }

        public string CommandName
        {
            get
            {
                if (ViewState["CommandName"] == null)
                {
                    ViewState["CommandName"] = "GridConfirmControl_Delete";
                }

                return Convert.ToString(ViewState["CommandName"]);
            }
            set
            {
                ViewState["CommandName"] = value;
                lnkDelete.CommandName = value;
            }
        }

        //public string ImageUrl
        //{
        //    get { return imgConfirm.ImageUrl; }
        //    set { imgConfirm.ImageUrl = value; }
        //}

        public string ToolTip
        {
            get { return hyperValue.ToolTip; }
            set { hyperValue.ToolTip = value; }
        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik, @toddanglin
        //Facebook: facebook.com/telerik
        //=======================================================


        protected void Page_Load(object sender, EventArgs e)
        {
            lnkDelete.CommandArgument = this.CommandArgument;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            OnDelete(lnkDelete.CommandArgument);
        }
    }
}