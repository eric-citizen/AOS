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
    public partial class exhibit_behaviors : MainBase
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
                    gvExhibitBehaviors.Columns[gvExhibitBehaviors.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "ExhibitID = " + this.ExhibitID + " AND Behavior LIKE '" + AlphabetFilter.Filter + "%'";
            }
            else
            {
                e.InputParameters["filterExpression"] = "ExhibitID = " + this.ExhibitID;
            }
        }

        protected void gvExhibitBehaviors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;
                CZDataObjects.ExhibitBehavior item = e.Row.DataItem as CZDataObjects.ExhibitBehavior;

                //lnkEdit.NavigateUrl = "~/admin/dialogs/edit-exhibit-behavior.aspx?exbId={0}".FormatWith(item.ExhibitBehaviorID);
                litExhibitName.Text = item.ExhibitName;
            }           

        }

        protected void gvExhibitBehaviors_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteBehavior" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ExhibitBehaviorList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior permanently deleted.", "Record Deleted");
            }

            gvExhibitBehaviors.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvExhibitBehaviors.DataKeys[row.RowIndex].Value);

            ExhibitBehavior cat = ExhibitBehaviorList.GetItem(id);
            cat.Active = cb.Checked;
            ExhibitBehaviorList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior inactivated successfully.", "Record Updated");
            }

            gvExhibitBehaviors.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvExhibitBehaviors.PageIndex = 0;
            gvExhibitBehaviors.DataBind();
        }
       
    }
}