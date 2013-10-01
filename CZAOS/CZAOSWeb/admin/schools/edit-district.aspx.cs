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

namespace CZAOSWeb.admin.schools
{
    public partial class edit_district : MainBase 
    {
        private int DistrictID
        {
            get
            {
                if (Request.QueryString.Contains("dId"))
                {
                    hdnItemID.Value = Request.QueryString["dId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.DistrictID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New District";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit District";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            SchoolDistrict item = SchoolDistrictList.GetItem(this.DistrictID);

            if (item == null)
            {
                this.DisplayError("District ID {0} not found!".FormatWith(this.DistrictID));
            }
            else
            {
                txtDistrict.Text = item.District;         
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                SchoolDistrict item = null;

                if (this.DistrictID == 0)
                {

                    item = new CZDataObjects.SchoolDistrict();

                    item.District = txtDistrict.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    SchoolDistrictList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "District created successfully", "Record Created");

                    

                }
                else    
                {
                    item = SchoolDistrictList.GetItem(this.DistrictID);

                    item.District = txtDistrict.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;

                    SchoolDistrictList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "District updated successfully", "Record Updated");

                    

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}