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
    public partial class edit_behavior_category : MainBase 
    {
        private int CategoryID
        {
            get
            {
                if (Request.QueryString.Contains("bcatId"))
                {
                    hdnItemID.Value = Request.QueryString["bcatId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.CategoryID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Behavior Category";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Behavior Category";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            BehaviorCategory item = BehaviorCategoryList.GetItem(this.CategoryID);

            if (item == null)
            {
                this.DisplayError("Category ID {0} not found!".FormatWith(this.CategoryID));
            }
            else
            {
                txtCategory.Text = item.BvrCat;
                txtCategoryCode.Text = item.BvrCatCode;
                txtDesc.Text = item.Description;

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
                BehaviorCategory item = null;

                if (BehaviorCategoryList.ItemExists(txtCategory.HtmlEncodedText().Trim(), txtCategoryCode.HtmlEncodedText().Trim(), CategoryID))
                {
                    this.DisplayError("This category name/code combination already exists.");
                    return;
                }

                if (this.CategoryID == 0)
                {
                    item = new BehaviorCategory();
                    item.BvrCat = txtCategory.HtmlEncodedText().Trim();
                    item.BvrCatCode = txtCategoryCode.HtmlEncodedText().Trim();
                    item.Description = txtDesc.HtmlEncodedText().Trim();

                    //This value is being inverted because of the wording. 
                    //The wording in the database is 'Mask', but the wording while creating the item is 'Show'
                    item.MaskAma = !chkMaskAma.Checked;
                    item.MaskProf = !chkMaskPro.Checked;


                    item.Active = chkActive.Checked;

                    BehaviorCategoryList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Category created successfully", "Record Created");

                    
                }
                else    
                {
                    item = BehaviorCategoryList.GetItem(this.CategoryID);

                    item.BvrCat = txtCategory.HtmlEncodedText().Trim();
                    item.BvrCatCode = txtCategoryCode.HtmlEncodedText().Trim();
                    item.Description = txtDesc.HtmlEncodedText().Trim();

                    //This value is being inverted because of the wording. 
                    //The wording in the database is 'Mask', but the wording while creating the item is 'Show'
                    item.MaskAma = !chkMaskAma.Checked;
                    item.MaskProf = !chkMaskPro.Checked;

                    item.Active = chkActive.Checked;

                    BehaviorCategoryList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Category updated successfully", "Record Updated");

                   
                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}