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

namespace CZAOSWeb.admin.Crowds
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvCrowds.Columns[gvCrowds.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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

          

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvCrowds.DataKeys[row.RowIndex].Value);

            Crowd cat = CrowdList.GetItem(id);
            cat.Active = cb.Checked;
            CrowdList.UpdateItem(cat);

            if (cat.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Crowd activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Crowd inactivated successfully.", "Record Updated");
            }

            gvCrowds.DataBind();
            
        }

        

        protected void gvCrowds_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCrowd" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CrowdList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Crowd permanently deleted.", "Record Deleted");
            }

            gvCrowds.DataBind();
        }

        protected void gvCrowds_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 
            }
        }
       
    }
}