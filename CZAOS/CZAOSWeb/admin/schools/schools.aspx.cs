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
    public partial class schools : MainBase
    {
        public int DistrictID 
        {
            get
            {
                if (Request.QueryString.Contains("dID"))
                {
                    hdnItemID.Value = Request.QueryString["dID"];
                }

                return hdnItemID.ItemID;
            }        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvSchools.Columns[gvSchools.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
                }
                litDistrict.Text = SchoolDistrictList.GetItem(DistrictID).District;
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
                    e.InputParameters["filterExpression"] = "School LIKE '" + AlphabetFilter.Filter + "%' AND DistrictID = " + DistrictID;
                }
                else
                {
                    e.InputParameters["filterExpression"] = "DistrictID = " + DistrictID;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("School LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("DistrictName LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("Active LIKE '%{0}%'", freeText);

                e.InputParameters["filterExpression"] = sb.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvSchools.DataBind();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtFreeText.Clear();
            gvSchools.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvSchools.DataBind();
        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int id = Convert.ToInt32(gvSchools.DataKeys[row.RowIndex].Value);

            School item = SchoolList.GetItem(id);
            item.Active = cb.Checked;
            SchoolList.UpdateItem(item);

            if (item.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "School activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "School inactivated successfully.", "Record Updated");
            }

            gvSchools.DataBind();

        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            gvSchools.PageIndex = 0;
            gvSchools.DataBind();
        }

        protected void gvSchools_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSchool" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                SchoolList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "School permanently deleted.", "Record Deleted");
            }

            gvSchools.DataBind();
        }

        protected void gvSchools_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;
                CZDataObjects.School item = e.Row.DataItem as CZDataObjects.School;

                //NavigateUrl='<%# Bind("BehaviorID","~/admin/dialogs/edit-behavior.aspx?bcatId=<%= this.CategoryID %>&bId={0}") %>'
                lnkEdit.NavigateUrl = "~/admin/schools/edit-school.aspx?schoolId={0}&districtId={1}".FormatWith(item.SchoolID, DistrictID);
                litDistrict.Text = item.DistrictName;
            }
        }

    }
}