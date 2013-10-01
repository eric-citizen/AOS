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

namespace CZAOSWeb.admin.weather
{
    public partial class wind : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvData.Columns[gvData.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "Wind LIKE '" + AlphabetFilter.Filter + "%'";
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
            int id = Convert.ToInt32(gvData.DataKeys[row.RowIndex].Value);

            Wind item = WindList.GetItem(id);
            item.Active = cb.Checked;
            WindList.UpdateItem(item);

            if (item.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Condition activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Condition inactivated successfully.", "Record Updated");
            }

            gvData.DataBind();

        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvData.PageIndex = 0;
            gvData.DataBind();
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteWind" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                WindList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Condition permanently deleted.", "Record Deleted");
            }

            gvData.DataBind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

    }
}