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
    public partial class edit_grade : MainBase 
    {
        private int GradeID
        {
            get
            {
                if (Request.QueryString.Contains("gId"))
                {
                    hdnItemID.Value = Request.QueryString["gId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.GradeID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Grade";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Grade";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            Grade item = GradeList.GetItem(this.GradeID);

            if (item == null)
            {
                this.DisplayError("Grade ID {0} not found!".FormatWith(this.GradeID));
            }
            else
            {
                txtGrade.Text = item.GradeName;         
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Grade item = null;

                if (this.GradeID == 0)
                {

                    item = new CZDataObjects.Grade();                     

                    item.GradeName = txtGrade.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    GradeList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Grade created successfully", "Record Created");

                   

                }
                else    
                {
                    item = GradeList.GetItem(this.GradeID);

                    item.GradeName = txtGrade.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;

                    GradeList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Grade updated successfully", "Record Updated");

                   
                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}