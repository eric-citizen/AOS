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
    public partial class edit_exhibit : MainBase 
    {

        private int ExhibitID
        {
            get
            {
                if (Request.QueryString.Contains("exId"))
                {
                    hdnItemID.Value = Request.QueryString["exId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.LoadRegions();
        }

        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!this.IsPostBack)
            {
                if (this.ExhibitID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Exhibit";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Exhibit";
                    this.LoadData();
                }
            }
        }

        private void LoadRegions()
        {
            AnimalRegionList regions = AnimalRegionList.GetActiveItemCollection();
            ddlRegion.DataSource = regions;
            ddlRegion.DataBind();

            ddlRegion.AddEmptyListItem("Select Region", "-1");

        }

        private void LoadData()
        {
            Exhibit item = ExhibitList.GetItem(this.ExhibitID);

            if (item == null)
            {
                this.DisplayError("Exhibit ID {0} not found!".FormatWith(this.ExhibitID));
            }
            else
            {
                txtExhibit.Text = item.ExhibitName;
                ddlRegion.SelectListItemByValue(item.AnimalRegionCode);                             

                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Exhibit item = null;

                if (ExhibitList.ItemExists(txtExhibit.HtmlEncodedText().Trim(), ddlRegion.SelectedValue.Trim(), ExhibitID))
                {
                    this.DisplayError("This exhibit already exists for this region.");
                    return;
                }

                if (this.ExhibitID == 0)
                {    
                    item = new CZDataObjects.Exhibit();
                    item.ExhibitName = txtExhibit.HtmlEncodedText().Trim();
                    item.AnimalRegionCode = ddlRegion.SelectedValue;                    
                    item.Active = chkActive.Checked;

                    ExhibitList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Exhibit created successfully", "Record Created");

                    

                }
                else    
                {
                    item = ExhibitList.GetItem(this.ExhibitID);

                    item.ExhibitName = txtExhibit.HtmlEncodedText().Trim();
                    item.AnimalRegionCode = ddlRegion.SelectedValue;
                    item.Active = chkActive.Checked;

                    ExhibitList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Exhibit updated successfully", "Record Updated");

                    

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}