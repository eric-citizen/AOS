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
    public partial class edit_location : MainBase 
    {
        private int LocationID
        {
            get
            {
                if (Request.QueryString.Contains("locId"))
                {
                    hdnItemID.Value = Request.QueryString["locId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.LocationID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Location";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Location";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            CZDataObjects.Location item = LocationList.GetItem(this.LocationID);

            if (item == null)
            {
                this.DisplayError("Location ID {0} not found!".FormatWith(this.LocationID));
            }
            else
            {
                txtDescription.Text = item.Description;
                txtLocationCode.Text = item.LocationCode;

                //This value is being inverted because of the wording. 
                //The wording in the database is 'Mask', but the wording while creating the item is 'Show'
                chkMaskAma.Checked = !item.MaskAma;
                chkMaskPro.Checked = !item.MaskProf;    
            
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                CZDataObjects.Location item = null;

                if (this.LocationID == 0)
                {
                    
                    item = new CZDataObjects.Location();
                     
                    item.LocationCode = txtLocationCode.HtmlEncodedText().Trim();
                    item.Description = txtDescription.HtmlEncodedText().Trim();

                    //This value is being inverted because of the wording. 
                    //The wording in the database is 'Mask', but the wording while creating the item is 'Show'
                    item.MaskAma = !chkMaskAma.Checked;
                    item.MaskProf = !chkMaskPro.Checked;

                    item.Active = chkActive.Checked;

                    LocationList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Location created successfully", "Record Created");

                    

                }
                else    
                {
                    item = LocationList.GetItem(this.LocationID);

                    item.LocationCode = txtLocationCode.HtmlEncodedText().Trim();
                    item.Description = txtDescription.HtmlEncodedText().Trim();

                    //This value is being inverted because of the wording. 
                    //The wording in the database is 'Mask', but the wording while creating the item is 'Show'
                    item.MaskAma = !chkMaskAma.Checked;
                    item.MaskProf = !chkMaskPro.Checked;

                    item.Active = chkActive.Checked;

                    LocationList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Location updated successfully", "Record Updated");

                    

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}