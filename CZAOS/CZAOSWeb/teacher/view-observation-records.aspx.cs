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

namespace CZAOSWeb.teacher.observation
{
    public partial class view_observation_records : MainBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (obs.ObserveType.ToString() == "School")
                gvObs.Columns[3].Visible = false;

            var metaString = obs.ObserveStart.ToShortDateString() + " // " + obs.ObservationID.ToString() + " // " + obs.ObserveType.ToString() + " // " + obs.Exhibit.ToString();
            litHeader.Text = metaString;
            litFooter.Text = metaString;

            
        }

        protected void cztDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                gvPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

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

        protected void cztDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["filterExpression"] = "ObservationID = '" + this.ObservationID + "'";
        }

        //protected void gvObsRec_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "DeleteObservation" && base.IsMasterAdmin)
        //    {
        //        int id = Convert.ToInt32(e.CommandArgument);
        //        ObservationList.DeleteItem(id);
        //        this.Toast(PageExtensions.ToastMessageType.success, "Observation permanently deleted.", "Record Deleted");
        //    }

        //    gvObs.DataBind();
        //}

        protected void gvObs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[1].Text != "")//Format the date correctly
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.ToDate().ToShortTimeString();


            var animal = AnimalList.GetItemByZooID(e.Row.Cells[2].Text);
            if (animal != null)
            {
                e.Row.Cells[2].Text = animal.HouseName;
            }
            //if (e.Row.Cells[1].Text != "")//Get animal Housename
            //    e.Row.Cells[1].Text = AnimalList.GetItemByZooID(e.Row.Cells[1].Text).HouseName;


            var id = e.Row.Cells[5].Text.ToInt32();

            if (id > 0)//Get location name.
            {
                var location = LocationList.Get(id);

                e.Row.Cells[5].Text = location.Description;
            }

        }

        protected void gvObs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "FlagRecord")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var ObsRec = ObservationRecordList.GetItem(id);

                if (ObsRec.Flagged == false)
                    ObsRec.Flagged = true;
                else
                    ObsRec.Flagged = false;

                ObservationRecordList.UpdateItem(ObsRec);
                this.Toast(PageExtensions.ToastMessageType.success, "Observation record has been flagged.", "Record Flagged");
            }

            gvObs.DataBind();
        }


    }
}