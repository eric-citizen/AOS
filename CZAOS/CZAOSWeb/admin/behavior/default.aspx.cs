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
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvBCat.Columns[gvBCat.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
            string freeText = txtFreeText.HtmlEncodedText();

            if (freeText.IsNullOrEmpty())
            {
                if (AlphabetFilter.Filter != AlphabetFilter.CLEAR_FILTER_KEY)
                {
                    e.InputParameters["filterExpression"] = "BvrCat LIKE '" + AlphabetFilter.Filter + "%'";
                }
                else
                {
                    e.InputParameters["filterExpression"] = string.Empty;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("BvrCat LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("BvrCatCode LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("Description LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("Active LIKE '%{0}%'", freeText);

                e.InputParameters["filterExpression"] = sb.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvBCat.DataBind();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtFreeText.Clear();
            gvBCat.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvBCat.DataBind();
        }

        protected void gvBCat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }           

        }

        protected void gvBCat_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteCategory" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                BehaviorCategoryList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior Category and related behaviors permanently deleted.", "Record Deleted");
            }

            gvBCat.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvBCat.DataKeys[row.RowIndex].Value);

            BehaviorCategory cat = BehaviorCategoryList.GetItem(id);
            cat.Active = cb.Checked;
            BehaviorCategoryList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior Category activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Behavior Category inactivated successfully.", "Record Updated");
            }

            gvBCat.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvBCat.PageIndex = 0;
            gvBCat.DataBind();
        }
       
    }
}