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
    public partial class edit_exhibit_behavior : MainBase 
    {

        private int BehaviorID
        {
            get
            {
                if (Request.QueryString.Contains("exbId"))
                {
                    hdnItemID.Value = Request.QueryString["exbId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        private int ExhibitID
        {
            get
            {
                if (Request.QueryString.Contains("exId"))
                {
                    hdnExhibitID.Value = Request.QueryString["exId"];
                }

                return hdnExhibitID.ItemID;
            }
            set
            {
                hdnExhibitID.ItemID = value;
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.LoadBehaviors();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.ExhibitID == 0 && this.BehaviorID == 0)
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

                    Exhibit exhibit = ExhibitList.GetItem(this.ExhibitID);
                    litName.Text = exhibit.ExhibitName;
                    
                }
                else
                {
                    fieldsetLegend.Text = "Edit Behavior";
                    this.LoadData();
                }
            }
        }

        private void LoadBehaviors()
        {
            ddlBehavior.DataSource = BehaviorList.GetActiveItemCollection();
            ddlBehavior.DataBind();
            ddlBehavior.AddEmptyListItem("Select Behavior", "-1");
        }

        private void LoadData()
        {
            ExhibitBehavior item = ExhibitBehaviorList.GetItem(this.BehaviorID);

            if (item == null)
            {
                this.DisplayError("Behavior ID {0} not found!".FormatWith(this.BehaviorID));
            }
            else
            {
                litName.Text = item.ExhibitName;
                ddlBehavior.SelectListItemByValue(item.BehaviorID.ToString());
                this.ExhibitID = item.ExhibitID;

                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                ExhibitBehavior item = null;

                if (this.BehaviorID == 0)
                {                


                    item = new CZDataObjects.ExhibitBehavior();
                    item.ExhibitID = this.ExhibitID;
                    item.BehaviorID = ddlBehavior.SelectedValue.ToInt32(); //txtBehaviorCode.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;                    

                    ExhibitBehaviorList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Behavior created successfully", "Record Created");

                    

                }
                else    
                {
                    item = ExhibitBehaviorList.GetItem(this.BehaviorID);

                    item.ExhibitID = this.ExhibitID;
                    item.BehaviorID = ddlBehavior.SelectedValue.ToInt32(); //txtBehaviorCode.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    ExhibitBehaviorList.UpdateItem(item);

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