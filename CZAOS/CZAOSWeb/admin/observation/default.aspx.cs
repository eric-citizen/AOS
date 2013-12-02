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
                AnimalListRec.DataSource = AnimalList.GetItemCollection();//Setup search items for Recent Observations
                AnimalListRec.DataTextField = "CommonName";
                AnimalListRec.DataValueField = "AnimalID";
                AnimalListRec.DataBind();
                AnimalListRec.SelectedIndex = 0;
                DistrictListRec.DataSource = SchoolDistrictList.GetItemCollection(0, 0, string.Empty, "Active = 1");
                DistrictListRec.DataTextField = "District";
                DistrictListRec.DataValueField = "DistrictID";
                DistrictListRec.DataBind();
                DistrictListRec.SelectedValue = "1";
                SchoolListRec.DataSource = SchoolList.GetItemCollection(0, 0, string.Empty, "DistrictID = " + DistrictListRec.SelectedValue);
                SchoolListRec.DataTextField = "SchoolName";
                SchoolListRec.DataValueField = "SchoolID";
                SchoolListRec.DataBind();


                AnimalListUp.DataSource = AnimalList.GetItemCollection(); //Setup search items for Upcoming Observations
                AnimalListUp.DataTextField = "CommonName";
                AnimalListUp.DataValueField = "AnimalID";
                AnimalListUp.DataBind();
                AnimalListUp.SelectedIndex = 0;
                DistrictListUp.DataSource = SchoolDistrictList.GetItemCollection(0, 0, string.Empty, "Active = 1");
                DistrictListUp.DataTextField = "District";
                DistrictListUp.DataValueField = "DistrictID";
                DistrictListUp.DataBind();
                DistrictListUp.SelectedValue = "1";
                SchoolListUp.DataSource = SchoolList.GetItemCollection(0, 0, string.Empty, "DistrictID = " + DistrictListUp.SelectedValue);
                SchoolListUp.DataTextField = "SchoolName";
                SchoolListUp.DataValueField = "SchoolID";
                SchoolListUp.DataBind();

                if (!base.IsMasterAdmin)
                {
                    gvObsRec.Columns[gvObsRec.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
                    gvObsUp.Columns[gvObsUp.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
                }
                if (base.IsObserver)
                {
                    gvObsRec.Columns[gvObsRec.Columns.Count - 2].Visible = false; //hide records column from all but master admins
                    gvObsRec.Columns[gvObsRec.Columns.Count - 3].Visible = false; //hide edit column from all but master admins
                    gvObsUp.Columns[gvObsUp.Columns.Count - 2].Visible = false; //hide records column from all but master admins
                    gvObsUp.Columns[gvObsUp.Columns.Count - 3].Visible = false; //hide edit column from all but master admins
                }
            }
        }

        protected void districtChange(Object sender, EventArgs e)
        {
            SchoolListRec.DataSource = SchoolList.GetItemCollection(0, 0, string.Empty, "DistrictID = " + DistrictListRec.SelectedValue);
            SchoolListRec.DataTextField = "SchoolName";//Adjust the school list to current district on recent observations
            SchoolListRec.DataValueField = "SchoolID";
            SchoolListRec.DataBind();

            SchoolListUp.DataSource = SchoolList.GetItemCollection(0, 0, string.Empty, "DistrictID = " + DistrictListUp.SelectedValue);
            SchoolListUp.DataTextField = "SchoolName";//Adjust the school list to current district on upcoming observations
            SchoolListUp.DataValueField = "SchoolID";
            SchoolListUp.DataBind();
        }

        protected void cztRecDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                recPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

        protected void cztUpDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                upPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

        protected void cztDataSource_Selecting_Recent(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime date = DateTime.Now;
            
            //it this isn't a postpack, then there are not any active filter or sort expressions
            if (!this.IsPostBack)
            {
                e.InputParameters["filterExpression"] = "ObserveStart < '" + date + "'";
            }
            else
            {
                string filterExpression = string.Empty;

                if (dateFromRec.Value == "" || dateToRec.Value == "")
                {
                    filterExpression += "ObserveStart < '" + date + "'";
                }
                else
                {
                    filterExpression += " ObserveStart > '" + dateFromRec.Value + "' AND ObserveStart < '" + dateToRec.Value + "'";
                }

                if (searchStudentObservationsRec.Checked)
                {
                    filterExpression += "AND ObserveType = 'School' ";
                    if(SchoolListRec.SelectedValue.IsNotNullOrEmpty())
                        filterExpression += "AND SchoolID = '" + SchoolListRec.SelectedValue + "'";
                }

                if (timedRec.Checked && behaviorRec.Checked)
                    filterExpression += "AND Category in ('Timed', 'Behavior')";
                else if (timedRec.Checked)
                    filterExpression += " AND Category = 'Timed'";
                else if (behaviorRec.Checked)
                    filterExpression += " AND Category = 'Behavior'";
                
                

                e.InputParameters["filterExpression"] = filterExpression;

            }
        }

        protected void cztDataSource_Selecting_Upcoming(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime date = DateTime.Now;

            //it this isn't a postpack, then there are not any active filter or sort expressions
            if (!this.IsPostBack)
            {
                e.InputParameters["filterExpression"] = "ObserveStart > '" + date + "'";
            }
            else
            {
                string filterExpression = string.Empty;

                if (dateFromUp.Value == "" || dateToUp.Value == "")
                {
                    filterExpression += "ObserveStart > '" + date + "'";
                }
                else
                {
                    filterExpression += "ObserveStart > '" + dateFromUp.Value + "' AND ObserveStart < '" + dateToUp.Value + "'";
                }

                if (searchStudentObservationsUp.Checked)
                {
                    filterExpression += "AND ObserveType = 'School' "; 
                    if(SchoolListUp.SelectedValue.IsNotNullOrEmpty())
                        filterExpression += "AND SchoolID = '" + SchoolListUp.SelectedValue + "'";
                }

                if (timedUp.Checked && behaviorUp.Checked)
                    filterExpression += "AND Category in ('Timed', 'Behavior')";
                else if (timedUp.Checked)
                    filterExpression += " AND Category = 'Timed'";
                else if (behaviorUp.Checked)
                    filterExpression += " AND Category = 'Behavior'";


                

                e.InputParameters["filterExpression"] = filterExpression;

            }
        }

        protected void gvObsRec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteObservation" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ObservationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Observation permanently deleted.", "Record Deleted");
            }

            gvObsRec.DataBind();
        }

        protected void gvObsUp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteObservation" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ObservationList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Observation permanently deleted.", "Record Deleted");
            }

            gvObsUp.DataBind();
        }

        protected void gvObsRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "School")
                {
                    e.Row.FindControl("lnkStart").Visible = false;
                }
                else
                {
                    var link = (HyperLink) e.Row.FindControl("lnkStart");
                    var category = link.NavigateUrl.ToLower();
                    if (category == "timed")
                    {
                        link.NavigateUrl = String.Format("/TimedProfessionalPage.html?observationId={0}",
                            e.Row.Cells[1].Text);
                    }
                    else
                    {
                        link.NavigateUrl = String.Format("/ProfessionalPage.html?observationId={0}",
                            e.Row.Cells[1].Text);
                    }
                }
            }
        }

        protected void gvObsUp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "School")
                {
                    e.Row.FindControl("lnkStart").Visible = false;
                }
                else
                {
                    var link = (HyperLink)e.Row.FindControl("lnkStart");
                    var category = link.NavigateUrl.ToLower();
                    if (category == "timed")
                    {
                        link.NavigateUrl = String.Format("/TimedProfessionalPage.html?observationId={0}",
                            e.Row.Cells[1].Text);
                    }
                    else
                    {
                        link.NavigateUrl = String.Format("/ProfessionalPage.html?observationId={0}",
                            e.Row.Cells[1].Text);
                    }
                }
            }
        }

        protected void searchRec_Click(object sender, EventArgs e)
        {
            gvObsRec.DataBind();
        }

        protected void searchUp_Click(object sender, EventArgs e)
        {
            gvObsUp.DataBind();
        }

        protected void searchStudentObservationsRec_CheckedChanged(object sender, EventArgs e)
        {
            if (searchStudentObservationsRec.Checked == true)
            {
                behaviorRec.Checked = false;
                timedRec.Checked = true;
                behaviorRec.Enabled = false;
                DistrictListRec.Enabled = true;
                SchoolListRec.Enabled = true;
            }
            else
            {
                DistrictListRec.Enabled = false;
                SchoolListRec.Enabled = false;
                behaviorRec.Enabled = true;
            }

        }

        protected void searchStudentObservationsUp_CheckedChanged(object sender, EventArgs e)
        {
            if (searchStudentObservationsUp.Checked == true)
            {
                behaviorUp.Checked = false;
                timedUp.Checked = true;
                behaviorUp.Enabled = false;
                DistrictListUp.Enabled = true;
                SchoolListUp.Enabled = true;
            }
            else
            {
                DistrictListUp.Enabled = false;
                SchoolListUp.Enabled = false;
                behaviorUp.Enabled = true;
            }

        }

        protected void clearRec_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx", false);
        }
        protected void clearUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx#UpcomingObservations", false);
        }

    }
}