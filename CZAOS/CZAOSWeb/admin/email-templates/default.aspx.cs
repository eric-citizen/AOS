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

namespace CZAOSWeb.admin.email_templates
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvEmailTemplate.Columns[gvEmailTemplate.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = "[key] LIKE '%{0}%' OR Subject Like '%{0}%'".FormatWith(AlphabetFilter.Filter);
            }
            else
            {
                e.InputParameters["filterExpression"] = string.Empty;
            }

        }

        protected void gvEmailTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }           

        }

        protected void gvEmailTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteTemplate" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmailTemplateList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Template permanently deleted.", "Record Deleted");
            }

            gvEmailTemplate.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvEmailTemplate.DataKeys[row.RowIndex].Value);

            EmailTemplate item = EmailTemplateList.GetItem(id);
            item.Active = cb.Checked;
            EmailTemplateList.UpdateItem(item);

            if (item.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Template activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Template inactivated successfully.", "Record Updated");
            }

            gvEmailTemplate.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvEmailTemplate.PageIndex = 0;
            gvEmailTemplate.DataBind();
        }
       
    }
}