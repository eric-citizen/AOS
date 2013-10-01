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
using CZDataObjects.Extensions;
using CZAOSCore.basepages;
using CZAOSWeb.controls;

namespace CZAOSWeb.admin.Location
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvLocation.Columns[gvLocation.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "Description LIKE '" + AlphabetFilter.Filter + "%'";
            }
            else
            {
                e.InputParameters["filterExpression"] = string.Empty;
            }

        }        

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvLocation.DataKeys[row.RowIndex].Value);

            CZDataObjects.Location cat = LocationList.GetItem(id);
            cat.Active = cb.Checked;
            LocationList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Location activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Location inactivated successfully.", "Record Updated");
            }

            gvLocation.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvLocation.PageIndex = 0;
            gvLocation.DataBind();
        }

        protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteLocation" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                LocationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Location permanently deleted.", "Record Deleted");
            }

            gvLocation.DataBind();
        }

        protected void gvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CZDataObjects.Location loc = e.Row.DataItem as CZDataObjects.Location;
                e.Row.Cells[2].Text = loc.MaskAma.ToYesNoString();
                e.Row.Cells[3].Text = loc.MaskProf.ToYesNoString();
            }
        }
       
    }
}