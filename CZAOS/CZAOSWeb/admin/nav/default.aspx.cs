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

namespace CZAOSWeb.admin.master
{
    public partial class nav : MainBase
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
            

        }       

        

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteNav" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                AdminNavigationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Nav item permanently deleted.", "Record Deleted");
            }

            gvData.DataBind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void sccInit_Confirm(object sender, EventArgs e)
        {
            AdminNavigationList.InitNav();
            gvData.DataBind();
            this.Toast(PageExtensions.ToastMessageType.success, "Nav items initialized.");
        }

    }
}