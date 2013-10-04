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

            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvObsRec.Columns[gvObsRec.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
                    gvObsUp.Columns[gvObsUp.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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

        protected void cztDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                gvPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

        protected void cztDataSource_Selecting_Recent(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (!this.IsPostBack)
            {
                e.InputParameters["filterExpression"] = "ObserveStart < '" + date + "'";
            }
            else
            {
                string includeSchool = string.Empty;
                if (includeStudentObservationsRec.Checked)
                    includeSchool = "ObservationType = School OR ObservationType = Professional"; 
                else
                    includeSchool = "ObservatoinType = Professional"; 

                string timedBehavior = string.Empty;
                if (timedRec.Checked)
                    timedBehavior = "Category = Timed";
                else if (behaviorRec.Checked)
                    timedBehavior = "Category = Behavior";
                else
                    timedBehavior = "Category = Timed OR Category = Behavior";

                string school = "School = " + SchoolListRec.SelectedValue;

                //string animal = "Animal = " + AnimalListRec.SelectedValue;

                string strDate = "ObserveStart > " + dateFromRec.Value + "AND ObserveStart < " + dateToRec.Value;


                e.InputParameters["filterExpression"] = includeSchool + "AND" + timedBehavior + "AND" + school + "AND" + strDate//+ "AND" +  animal
                    + "AND" + "'ObserveStart < '" + date + "'";
            }
        }

        protected void cztDataSource_Selecting_Upcoming(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (!this.IsPostBack)
            {
                e.InputParameters["filterExpression"] = "ObserveStart > '" + date + "'";
            }
            else
            {
                e.InputParameters["filterExpression"] = "ObserveStart < '" + date + "'";
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

            }
        }

        protected void gvObsUp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
       
    }
}