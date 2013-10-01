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
    public partial class edit_wind : MainBase 
    {
        private int WindID
        {
            get
            {
                if (Request.QueryString.Contains("windId"))
                {
                    hdnItemID.Value = Request.QueryString["windId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.WindID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Wind Condition";
                     
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
            Wind item = WindList.GetItem(this.WindID);

            if (item == null)
            {
                this.DisplayError("Wind ID {0} not found!".FormatWith(this.WindID));
            }
            else
            {
                txtCondition.Text = item.Description;         
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Wind item = null;

                if (this.WindID == 0)
                {

                    item = new Wind();

                    item.Description = txtCondition.HtmlEncodedText().Trim();
                    item.Active = chkActive.Checked;

                    WindList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Condition created successfully", "Record Created");

                   
                }
                else    
                {
                    item = WindList.GetItem(this.WindID);

                    item.Description = txtCondition.HtmlEncodedText().Trim();                    
                    item.Active = chkActive.Checked;

                    WindList.UpdateItem(item);

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