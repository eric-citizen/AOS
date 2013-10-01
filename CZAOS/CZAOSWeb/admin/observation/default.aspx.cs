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
    public partial class _default : MainBase
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
            if (AlphabetFilter.Filter != AlphabetFilter.CLEAR_FILTER_KEY)
            {
                e.InputParameters["filterExpression"] = "School LIKE '" + AlphabetFilter.Filter + "%'";
            }
            else
            {
                e.InputParameters["filterExpression"] = string.Empty;
            }

        }      
        
        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvObs.PageIndex = 0;
            gvObs.DataBind();
        }

        protected void gvObs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteObservation" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ObservationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Observation permanently deleted.", "Record Deleted");
            }

            gvObs.DataBind();
        }

        protected void gvObs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 
            }
        }
       
    }
}