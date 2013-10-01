using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;

namespace CZAOSWeb.admin.dialogs
{
    public partial class edit_crowd : MainBase 
    {
        private int CrowdID
        {
            get
            {
                if (Request.QueryString.Contains("cId"))
                {
                    hdnItemID.Value = Request.QueryString["cId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.CrowdID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Crowd";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Crowd";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            Crowd item = CrowdList.GetItem(this.CrowdID);

            if (item == null)
            {
                this.DisplayError("Crowd ID {0} not found!".FormatWith(this.CrowdID));
            }
            else
            {
                txtCrowd.Text = item.CrowdName;         
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Crowd item = null;

                if (this.CrowdID == 0)
                {

                    item = new CZDataObjects.Crowd();                     

                    item.CrowdName = txtCrowd.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    CrowdList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Crowd created successfully", "Record Created");

                   

                }
                else    
                {
                    item = CrowdList.GetItem(this.CrowdID);

                    item.CrowdName = txtCrowd.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;

                    CrowdList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Crowd updated successfully", "Record Updated");

                    

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}