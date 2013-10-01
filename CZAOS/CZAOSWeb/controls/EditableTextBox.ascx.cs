using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using KT.Extensions;

namespace CZAOS.controls
{
    public partial class EditableTextBox : System.Web.UI.UserControl
    {
        public event EventHandler EditableTextBoxUpdate;

        // this is the function that raises the event
        private void OnUpdate(string arg)
        {
            RowCommandEventHandler args = new RowCommandEventHandler(arg, this.CommandName);

            if (EditableTextBoxUpdate != null)
            {
                EditableTextBoxUpdate(this, args);
            }

        }

        public string Text
        {
            get { return hyperValue.Text; }
            set { hyperValue.Text = value; }
        }       

        public string UpdatedText
        {
            get
            {
                if (ViewState["UpdatedText"] == null)
                {
                    ViewState["UpdatedText"] = string.Empty;
                }

                return Convert.ToString(ViewState["UpdatedText"]);
            }
            set { ViewState["UpdatedText"] = value; }
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
                lnkUpdate.CommandArgument = value;
            }
        }


        public string CommandName
        {
            get
            {
                if (ViewState["CommandName"] == null)
                {
                    ViewState["CommandName"] = "EditableTextBox_Update";
                }

                return Convert.ToString(ViewState["CommandName"]);
            }
            set
            {
                ViewState["CommandName"] = value;
                lnkUpdate.CommandName = value;
            }
        }

        public bool IsValid
        {
            get { return this.UpdatedText.IsNotNullOrEmpty(); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.rfvWrite.ValidationGroup = this.ClientID;
            this.txtWrite.ValidationGroup = this.ClientID;
            this.lnkUpdate.ValidationGroup = this.ClientID;

            lnkUpdate.CommandArgument = this.CommandArgument;

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            this.UpdatedText = txtWrite.Text;
            this.OnUpdate(this.UpdatedText);

            hyperValue.Text = txtWrite.Text;
            txtWrite.Text = string.Empty;
            //editbox.Visible = false; !NO!!! this then does not render the editbox!
            hyperValue.Visible = true;
        }

        private void hyperValue_PreRender(object sender, System.EventArgs e)
        {
            if (hyperValue.Text.IsNullOrEmpty())
            {
                hyperValue.Text = "[Empty]";
            }

        }

    }
}