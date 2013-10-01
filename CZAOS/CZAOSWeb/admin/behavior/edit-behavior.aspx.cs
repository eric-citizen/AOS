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
    public partial class edit_behavior : MainBase 
    {

        private int BehaviorID
        {
            get
            {
                if (Request.QueryString.Contains("bId"))
                {
                    hdnItemID.Value = Request.QueryString["bId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        private int CategoryID
        {
            get
            {
                if (Request.QueryString.Contains("bcatId"))
                {
                    hdnCategoryID.Value = Request.QueryString["bcatId"];
                }

                return hdnCategoryID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.CategoryID == 0)
            {
                Dialog dialog = this.Master as Dialog;
                dialog.RefreshParent();
                return;
            }

            if (!this.IsPostBack)
            {
                if (this.BehaviorID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Behavior";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Behavior";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            CZDataObjects.Behavior item = BehaviorList.GetItem(this.BehaviorID);

            if (item == null)
            {
                this.DisplayError("Behavior ID {0} not found!".FormatWith(this.BehaviorID));
            }
            else
            {
                txtBehavior.Text = item.BehaviorName;
                txtBehaviorCode.Text = item.BehaviorCode;
                txtDesc.Text = item.Description;
                              

                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                CZDataObjects.Behavior item = null;

                if (this.BehaviorID == 0)
                {                   
    //                @Behavior varchar(100),
    //@BvrCatID int,
    //@BehaviorCode varchar(3),
    //@Description varchar(500),
    //@SortOrder int,
    //@Active bit

                    item = new CZDataObjects.Behavior();
                    item.BehaviorName = txtBehavior.HtmlEncodedText().Trim();
                    item.BehaviorCode = txtBehaviorCode.HtmlEncodedText().Trim();
                    item.Description = txtDesc.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;
                    item.BvrCatID = this.CategoryID;

                    BehaviorList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Behavior created successfully", "Record Created");

                   

                }
                else    
                {
                    item = BehaviorList.GetItem(this.BehaviorID);

                    item.BehaviorName = txtBehavior.HtmlEncodedText().Trim();
                    item.BehaviorCode = txtBehaviorCode.HtmlEncodedText().Trim();
                    item.Description = txtDesc.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;
                    item.BvrCatID = this.CategoryID;

                    BehaviorList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Behavior updated successfully", "Record Updated");

                   
                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}