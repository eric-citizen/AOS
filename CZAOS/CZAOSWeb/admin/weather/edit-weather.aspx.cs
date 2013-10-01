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
using CZAOSCore.basepages;
using CZAOSWeb.masterpages;

namespace CZAOSWeb.admin.weather
{
    public partial class edit_weather : MainBase 
    {
        private int WeatherID
        {
            get
            {
                if (Request.QueryString.Contains("weatherId"))
                {
                    hdnItemID.Value = Request.QueryString["weatherId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.WeatherID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Weather Condition";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Condition";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            WeatherCondition item = WeatherConditionList.GetItem(this.WeatherID);

            if (item == null)
            {
                this.DisplayError("Weather ID {0} not found!".FormatWith(this.WeatherID));
            }
            else
            {
                txtCondition.Text = item.Weather;         
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                WeatherCondition item = null;

                if (this.WeatherID == 0)
                {

                    item = new WeatherCondition();

                    item.Weather = txtCondition.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    WeatherConditionList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Condition created successfully", "Record Created");


                }
                else    
                {
                    item = WeatherConditionList.GetItem(this.WeatherID);

                    item.Weather = txtCondition.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;

                    WeatherConditionList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Condition updated successfully", "Record Updated");


                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}