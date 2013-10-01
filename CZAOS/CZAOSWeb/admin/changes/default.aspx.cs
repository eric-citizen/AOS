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

namespace CZAOSWeb.admin.changes
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvLog.Columns[gvLog.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                e.InputParameters["filterExpression"] = string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("UserDisplayName LIKE '%{0}%' OR ", freeText);                
                sb.AppendFormat("ChangeType LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("Identifier LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("Changes LIKE '%{0}%' ", freeText);

                e.InputParameters["filterExpression"] = sb.ToString();
            }

      //     [ID]
      //,[ChangeDate]
      //,[UserID]
      //,[UserDisplayName]
      //,[ChangeType]
      //,[Identifier]
      //,[Changes]
      //  
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvLog.DataBind();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtFreeText.Clear();
            gvLog.DataBind();
        }

        //protected void gvLog_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
             
        //}

        //protected void gvCrowds_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //Object Type: Wind, ID: 5
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
                 
        //    }
        //}
       
    }
}