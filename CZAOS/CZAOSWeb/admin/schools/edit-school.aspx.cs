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
    public partial class edit_school : MainBase 
    {
        private int SchoolID
        {
            get
            {
                if (Request.QueryString.Contains("schoolId"))
                {
                    hdnItemID.Value = Request.QueryString["schoolId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.LoadDistricts();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.SchoolID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New School";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit School";
                    this.LoadData();
                }
            }
        }

        private void LoadDistricts()
        {
            SchoolDistrictList districts = SchoolDistrictList.GetItemCollection(true);
            ddlDistrict.DataSource = districts;
            ddlDistrict.DataBind();

            ddlDistrict.AddEmptyListItem("Select District", "-1");

        }

        private void LoadData()
        {
            School item = SchoolList.GetItem(this.SchoolID);

            if (item == null)
            {
                this.DisplayError("School ID {0} not found!".FormatWith(this.SchoolID));
            }
            else
            {
                txtSchool.Text = item.SchoolName;         
                chkActive.Checked = item.Active;
                ddlDistrict.SelectListItemByValue(item.DistrictID.ToString()); 
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {               

                if (this.SchoolID == 0)
                {

                    School item = new CZDataObjects.School();                     

                    item.SchoolName = txtSchool.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;
                    item.DistrictID = ddlDistrict.SelectedValue.ToInt32();   

                    SchoolList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "School created successfully", "Record Created");

                    

                }
                else    
                {
                    School item = SchoolList.GetItem(this.SchoolID);

                    item.SchoolName = txtSchool.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;
                    item.DistrictID = ddlDistrict.SelectedValue.ToInt32();   

                    SchoolList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "School updated successfully", "Record Updated");

                   

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}