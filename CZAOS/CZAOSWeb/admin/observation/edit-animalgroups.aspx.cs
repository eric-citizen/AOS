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

namespace CZAOSWeb.admin.observation
{
    public partial class edit_animalgroups : MainBase 
    {
        private int TimedInfoID
        {
            get
            {
                if (Request.QueryString.Contains("timedinfoID"))
                {
                    string s = Request.QueryString["timedinfoID"];
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

        //protected override void Page_Init(object sender, EventArgs e)
        //{
        //    base.Page_Init(sender, e);
        //    this.LoadRegions();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.TimedInfoID == 0)
                {
                    //add a new animal!
                    fieldsetLegend.Text = "Add New Timed Info";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Timed Info";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            TimedInfo ti = TimedInfoList.GetItem(this.TimedInfoID);

            if (ti == null)
            {
                this.DisplayError("TimedInfo ID {0} not found!".FormatWith(this.TimedInfoID));
            }
            else
            {
                txtStartTime.Text = ti.TimeStart.ToString("hh:mmtt");
                txtEndTime.Text = ti.TimeEnd.ToString("hh:mmtt");
                litInterval.Text = ti.Interval.ToString();
                chkActive.Checked = ti.Active;
            }

        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                TimedInfo ti = null;               

                if (this.TimedInfoID == 0)
                {

                    ti = new TimedInfo();

                    ti.TimeStart = txtStartTime.Text.ToDate();
                    ti.TimeEnd = txtEndTime.Text.ToDate();
                    //litInterval.Text = ti.Interval.ToString();
                    ti.Active = chkActive.Checked;

                    TimedInfoList.AddItem(ti);

                    this.Toast(PageExtensions.ToastMessageType.success, "Timed Info created successfully", "Record Created");

                    

                }
                else
                {
                    ti = TimedInfoList.GetItem(this.TimedInfoID);

                    ti.TimeStart = txtStartTime.Text.ToDate();
                    ti.TimeEnd = txtEndTime.Text.ToDate();
                    //litInterval.Text = ti.Interval.ToString();
                    ti.Active = chkActive.Checked;

                    TimedInfoList.UpdateItem(ti);

                    this.Toast(PageExtensions.ToastMessageType.success, "Timed Info updated successfully", "Record Updated");

                   

                }

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }

    }

}