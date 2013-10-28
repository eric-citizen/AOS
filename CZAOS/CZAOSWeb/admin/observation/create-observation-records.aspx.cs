using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore;
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;
using CZAOS.controls;

namespace CZAOSWeb.admin.observation
{
    public partial class create_observation_records : System.Web.UI.Page
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

        private Observation obs
        {
            get
            {
                return ObservationList.GetItem(ObservationID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            txtStartTime.Text = obs.ObserveStart.ToShortTimeString();

            LoadEnvironment();
        }

        protected void LoadEnvironment()
        {
            ddlWeather.DataSource = WeatherConditionList.GetActive();
            ddlWeather.DataBind();
            ddlWeather.AddEmptyListItem("Select Weather Condition", "-1");

            ddlWind.DataSource = WindList.GetActive();
            ddlWind.DataBind();
            ddlWind.AddEmptyListItem("Select Wind", "-1");

            ddlCrowd.DataSource = CrowdList.GetActive();
            ddlCrowd.DataBind();
            ddlCrowd.AddEmptyListItem("Select Crowd Size", "-1");
        }

        protected void GetEnvironment()
        {
            var environment = WeatherList.GetItem(ObservationID);

            litTemp.Text = environment.Temperature.ToString();
            litWind.Text = environment.Wind;
            litWeatherCond.Text = environment.WeatherDescription;
            litCrowd.Text = environment.Crowd;
        }

        protected void btnSubEnv_Click(object sender, EventArgs e)
        {
            var weather = new Weather();

            weather.ObservationID = ObservationID;
            weather.Username = this.User.Identity.Name;
            weather.WeatherConditionID = ddlWeather.SelectedValue.ToInt32();
            weather.Temperature = txtTemp.Text.ToInt32();
            weather.WindID = ddlWeather.SelectedValue.ToInt32();
            weather.CrowdID = ddlCrowd.SelectedValue.ToInt32();
            weather.WeatherTime = Convert.ToDateTime(txtStartTime.Text);
            weather.Deleted = false;
            weather.Flagged = false;

            WeatherList.AddItem(weather);

            mvObs.ActiveViewIndex = 1;

            GetEnvironment();
        }
    }
}