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
    public partial class edit_region : MainBase 
    {
        private string RegionID
        {
            get
            {
                if (Request.QueryString.Contains("regionId"))
                {
                    hdnID.Value = Request.QueryString["regionId"]; 
                }

                return hdnID.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.RegionID.IsNullOrEmpty())
                {
                    //add a new region!
                    fieldsetLegend.Text = "Add New Animal Region";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Animal Region";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            AnimalRegion region = AnimalRegionList.GetItem(this.RegionID);

            if (region == null)
            {
                this.DisplayError("Region ID {0} not found!".FormatWith(this.RegionID));
            }
            else
            {
                txtRegionCode.Text = region.AnimalRegionCode;
                txtRegionCode.ReadOnly = true;
                txtRegionCode.CssClass = txtRegionCode.CssClass + " input-read-only";
                //unique-regioncode
                txtRegionCode.CssClass = txtRegionCode.CssClass.Replace("unique-regioncode", "");

                txtRegionName.Text = region.RegionName;
                txtAnimalRegion.Text = region.AnimalRegionName;

                chkActive.Checked = region.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                AnimalRegion region = null;

                //PageMessageOptions options = new PageMessageOptions();
                //options.OpaqueBg = false;
                //options.UrlRedirect = "function:refreshParent()";

                if (this.RegionID.IsNullOrEmpty())
                {
                    if (AnimalRegionList.RegionCodeExists(txtRegionCode.Text))
                    {
                        this.DisplayError("Region Code {0} already exists. Please enter a new code or return to the animal regions page to edit this code.".FormatWith(this.RegionID));
                        return;
                    }

                    region = new AnimalRegion();
                    region.AnimalRegionCode = txtRegionCode.HtmlEncodedText().Trim();
                    region.RegionName = txtRegionName.HtmlEncodedText().Trim();
                    region.AnimalRegionName = txtAnimalRegion.HtmlEncodedText().Trim();

                    region.Active = chkActive.Checked;

                    AnimalRegionList.AddItem(region);

                    //this.DisplayMessage("Region created successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Region created successfully", "Record Created");

                    
                }
                else    
                {
                    region = AnimalRegionList.GetItem(this.RegionID);

                    region.RegionName = txtRegionName.HtmlEncodedText().Trim();
                    region.AnimalRegionName = txtAnimalRegion.HtmlEncodedText().Trim();

                    region.Active = chkActive.Checked;

                    AnimalRegionList.UpdateItem(region);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Region updated successfully", "Record Updated");

                  

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}