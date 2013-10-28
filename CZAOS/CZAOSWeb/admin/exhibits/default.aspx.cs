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

namespace CZAOSWeb.admin.Exhibits
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvExhibit.Columns[gvExhibit.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "Exhibit LIKE '" + AlphabetFilter.Filter + "%'";
            }
            else
            {
                e.InputParameters["filterExpression"] = string.Empty;
            }

        }

        protected void gvExhibit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }           

        }

        protected void gvExhibit_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteExhibit" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ExhibitList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Exhibit and related behaviors permanently deleted.", "Record Deleted");
            }

            gvExhibit.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvExhibit.DataKeys[row.RowIndex].Value);

            Exhibit item = ExhibitList.GetItem(id);
            item.Active = cb.Checked;
            ExhibitList.UpdateItem(item);

            if (item.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Exhibit activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Exhibit inactivated successfully.", "Record Updated");
            }

            gvExhibit.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvExhibit.PageIndex = 0;
            gvExhibit.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvExhibit.DataBind();
        }
       
    }
}