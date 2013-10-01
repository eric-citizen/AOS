using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;
using CZAOSWeb.controls;

namespace CZAOSWeb.admin.Behavior
{
    public partial class exhibit_locations : MainBase
    {

        public int ExhibitID
        {
            get
            {
                if (Request.QueryString.Contains("exId"))
                {
                    hdnExhibitID.Value = Request.QueryString["exId"];
                }

                return hdnExhibitID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvExhibitLocations.Columns[gvExhibitLocations.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
                }

                if (this.ExhibitID == 0)
                {
                    Response.Redirect("default.aspx");
                }
                
            }
        }

        protected void cztDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                gvPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

        protected void cztDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (AlphabetFilter.Filter != AlphabetFilter.CLEAR_FILTER_KEY)
            {
                e.InputParameters["filterExpression"] = "ExhibitID = " + this.ExhibitID + " AND Location LIKE '" + AlphabetFilter.Filter + "%'";
            }
            else
            {
                e.InputParameters["filterExpression"] = "ExhibitID = " + this.ExhibitID;
            }
        }

        protected void gvExhibitLocations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;
                CZDataObjects.ExhibitLocation item = e.Row.DataItem as CZDataObjects.ExhibitLocation;

                //lnkEdit.NavigateUrl = "~/admin/dialogs/edit-exhibit-behavior.aspx?exbId={0}".FormatWith(item.ExhibitBehaviorID);
                litExhibitName.Text = item.Exhibit;
            }           

        }

        protected void gvExhibitLocations_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteLocation" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ExhibitLocationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Location permanently deleted.", "Record Deleted");
            }

            gvExhibitLocations.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvExhibitLocations.DataKeys[row.RowIndex].Value);

            ExhibitLocation cat = ExhibitLocationList.GetItem(id);
            cat.Active = cb.Checked;
            ExhibitLocationList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Location activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Location inactivated successfully.", "Record Updated");
            }

            gvExhibitLocations.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvExhibitLocations.PageIndex = 0;
            gvExhibitLocations.DataBind();
        }
       
    }
}