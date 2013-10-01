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
    public partial class tracking : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    Response.Redirect("/admin/");
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
                e.InputParameters["filterExpression"] = "[To] LIKE '%{0}%' OR Subject Like '%{0}%'".FormatWith(AlphabetFilter.Filter);
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
                EmailTracking item = e.Row.DataItem as EmailTracking;
                Image imgSent = e.FindControl<Image>("imgSent");
                Image imgFail = e.FindControl<Image>("imgFail");

                imgSent.Visible = item.Sent;
                imgFail.Visible = !imgSent.Visible;

                if (item.OpenDate.IsBeforeDate(new DateTime(2000, 1, 1)))
                {
                    e.Row.Cells[4].Text = string.Empty;
                }
            }           

        }

        protected void gvEmailTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteItem" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmailTrackingList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Tracking item permanently deleted.", "Record Deleted");
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