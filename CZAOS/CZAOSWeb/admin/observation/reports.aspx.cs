using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore;
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;

namespace CZAOSWeb.admin.observation
{
    public partial class observation_reports : MainBase
    {
        private int ObservationID
        {
            get
            {
                if (Request.QueryString.Contains("id"))
                {
                    string s = Request.QueryString["id"];
                    if (s.IsNumeric())
                    {
                        hdnID.Value = s;
                    }
                    else
                        hdnID.Value = 0.ToString();

                }
                else
                {
                    hdnID.Value = 0.ToString();
                }

                return hdnID.Value.ToInt32();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!this.IsPostBack)
            {                
                this.LoadData();
            }
        }

        private void LoadData()
        {
            if (this.ObservationID == 0)
            {                
                this.DisplayError("No observation found - missing id", "function:CZAOSUIDialogs.dialogCloseUIDialog();");
            }
            else
            {
                Observation obs = ObservationList.Get(this.ObservationID);
                Session.Set<int>(SessionKeys.UploadFileFolder, this.ObservationID);

                if (obs == null)
                {
                    this.DisplayError("No observation found - id {0} does not exist".FormatWith(this.ObservationID), "function:CZAOSUIDialogs.dialogCloseUIDialog();");
                }
                else
                {                    

                }

            }
        }

        
    }
}