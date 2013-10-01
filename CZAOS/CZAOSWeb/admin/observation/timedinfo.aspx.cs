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

namespace CZAOSWeb.admin.observation
{
    public partial class timedinfo : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvObs.Columns[gvObs.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
        
        

        protected void gvObs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTimedInfo" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                TimedInfoList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Timed Info permanently deleted.", "Record Deleted");
            }

            gvObs.DataBind();
        }

        protected void gvObs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 
            }
        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvObs.DataKeys[row.RowIndex].Value);

            TimedInfo ti = TimedInfoList.GetItem(id);
            ti.Active = cb.Checked;
            TimedInfoList.UpdateItem(ti);

            if (ti.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Record activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Record inactivated successfully.", "Record Updated");
            }

            gvObs.DataBind();

        }
       
    }
}