using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore;
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;

namespace CZAOSWeb.admin.observation
{
    public partial class view_observation : MainBase
    {
        private int ObservationID
        {
            get
            {
                if (Request.QueryString.Contains("observationID"))
                {
                    string s = Request.QueryString["observationID"];
                    if (s.IsNumeric())
                    {
                        hdnID.Value = s;
                    }
                    else
                        hdnID.Value = 0.ToString();

                }
                else
                {
                    hdnID.Value = 0.ToString();
                }

                return hdnID.Value.ToInt32();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {                
                this.LoadData();
            }
        }

        private void LoadData()
        {
            if (this.ObservationID == 0)
            {                
                this.DisplayError("No observation found - missing id", "function:CZAOSUIDialogs.dialogCloseUIDialog();");
            }
            else
            {
                Observation obs = ObservationList.Get(this.ObservationID);

                if (obs == null)
                {
                    this.DisplayError("No observation found - id {0} does not exist".FormatWith(this.ObservationID), "function:CZAOSUIDialogs.dialogCloseUIDialog();");
                }
                else
                {


                    var strDetail = obs.ObserveStart.ToShortDateString() + " // " + obs.ObservationID + " // " + obs.ObservationTypeName + " // " + obs.Exhibit;
                    litHead.Text = strDetail;
                    litFoot.Text = strDetail;
                    lnkHeadEdit.NavigateUrl = String.Format("~/admin/observation/edit-observation.aspx?observationId={0}", obs.ObservationID);
                    lnkFootEdit.NavigateUrl = String.Format("~/admin/observation/edit-observation.aspx?observationId={0}", obs.ObservationID);
                    lnkHeadRecords.NavigateUrl = String.Format("~/admin/observation/view-observation-records.aspx?observationId={0}", obs.ObservationID);
                    lnkFootRecords.NavigateUrl = String.Format("~/admin/observation/view-observation-records.aspx?observationId={0}", obs.ObservationID);

                    fieldsetLegend.Text = obs.ObservationTypeName;
                    litDate.Text = obs.ObserveStart.ToShortDateString();
                    litStart.Text = obs.ObserveStart.ToShortTimeString();
                    litEnd.Text = obs.ObserveEnd.ToShortTimeString();
                    litCategory.Text = obs.Category;
                    pnlTimer.Visible = litCategory.Text.Equals("Timed");
                    
                    if (pnlTimer.Visible)
                    {
                        litInterval.Text = obs.Interval.ToString() +" minutes.";
                        litTimer.Text = obs.Timer ? "Yes" : "No";
                        litManual.Text = obs.Manual ? "Yes" : "No";
                    }

                    ObserverList observers = ObserverList.GetActive(obs.ObservationID);
                    litObsCount.Text = obs.ObserverNo.ToString();
                    rptObservers.DataSource = observers;
                    rptObservers.DataBind();

                    litRegion.Text = obs.AnimalRegion;
                    litExhibit.Text = obs.Exhibit;

                    rptAnimal.DataSource = AnimalObservationList.GetActive(this.ObservationID);
                    rptAnimal.DataBind();

                    rptGroups.DataSource = AnimalGroupList.GetActiveByObservationID(obs.ObservationID);
                    rptGroups.DataBind();

                    if (obs.ObserveType == Observation.ObservationTypeEnum.Professional)
                    {
                        pnlSchool.Visible = false;
                        pnlLogin.Visible = false;
                    }
                    else
                    {
                        pnlSchool.Visible = true;
                        pnlLogin.Visible = true;
                        pnlAttending.Visible = false;
                        pnlManTimer.Visible = false;

                        litTeacherName.Text = obs.TeacherName;
                        litTeacherLogin.Text = obs.TeacherLogin;
                        litTeacherPassword.Text = obs.TeacherPass;
                        litStudentPassword.Text = obs.StudentPass;

                        litDistrict.Text = obs.District;
                        litSchool.Text = obs.School;
                        litGrade.Text = obs.Grade;

                    }

                }

            }
        }

        protected void rptGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptGroupAnimals = e.FindControl<Repeater>("rptGroupAnimals");
            AnimalGroup group = e.Item.DataItem as AnimalGroup;
            AnimalObservationList list = AnimalObservationList.GetActive(this.ObservationID, group.GrpID);

            rptGroupAnimals.DataSource = list;
            rptGroupAnimals.DataBind();
        }

        protected void btnHeadDelete_Click(object sender, EventArgs e)
        {
            if (base.IsMasterAdmin)
            {
                Observation obs = ObservationList.Get(this.ObservationID);
                ObservationList.DeleteItem(obs);
                Response.Redirect("default.aspx");
            }
        }
    }
}