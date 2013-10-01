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

namespace CZAOSWeb.admin.schools
{
    public partial class districts : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvDistricts.Columns[gvDistricts.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "District LIKE '" + AlphabetFilter.Filter + "%'";
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
            int id = Convert.ToInt32(gvDistricts.DataKeys[row.RowIndex].Value);

            SchoolDistrict cat = SchoolDistrictList.GetItem(id);
            cat.Active = cb.Checked;
            SchoolDistrictList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "District activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "District inactivated successfully.", "Record Updated");
            }

            gvDistricts.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvDistricts.PageIndex = 0;
            gvDistricts.DataBind();
        }

        protected void gvDistricts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDistrict" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                SchoolDistrictList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "District permanently deleted.", "Record Deleted");
            }

            gvDistricts.DataBind();
        }

        protected void gvDistricts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 
            }
        }
       
    }
}