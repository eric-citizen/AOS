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
    public partial class edit_exhibit_location : MainBase 
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
            this.LoadLocations();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.ExhibitID == 0 && this.LocationID == 0)
            {
                Dialog dialog = this.Master as Dialog;
                dialog.RefreshParent();
                return;
            }

            if (!this.IsPostBack)
            {
                if (this.LocationID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Location";

                    Exhibit exhibit = ExhibitList.GetItem(this.ExhibitID);
                    litName.Text = exhibit.ExhibitName;
                    
                }
                else
                {
                    fieldsetLegend.Text = "Edit Location";
                    this.LoadData();
                }
            }
        }

        private void LoadLocations()
        {
            ddlLocation.DataSource = LocationList.GetItemCollection(true);
            ddlLocation.DataBind();
            ddlLocation.AddEmptyListItem("Select Location", "-1");
        }

        private void LoadData()
        {
            ExhibitLocation item = ExhibitLocationList.GetItem(this.LocationID);

            if (item == null)
            {
                this.DisplayError("Location ID {0} not found!".FormatWith(this.LocationID));
            }
            else
            {
                litName.Text = item.Exhibit;
                ddlLocation.SelectListItemByValue(item.LocationID.ToString());
                this.ExhibitID = item.ExhibitID;

                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                ExhibitLocation item = null;

                if (this.LocationID == 0)
                {               

                    item = new CZDataObjects.ExhibitLocation();
                    item.ExhibitID = this.ExhibitID;
                    item.LocationID = ddlLocation.SelectedValue.ToInt32(); //txtBehaviorCode.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;                    

                    ExhibitLocationList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Location created successfully", "Record Created");

                    

                }
                else    
                {
                    item = ExhibitLocationList.GetItem(this.LocationID);

                    item.ExhibitID = this.ExhibitID;
                    item.LocationID = ddlLocation.SelectedValue.ToInt32();
                    item.Active = chkActive.Checked;

                    ExhibitLocationList.UpdateItem(item);

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