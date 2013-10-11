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
using CZAOSCore.basepages;
using CZAOSWeb.controls;

namespace CZAOSWeb.admin.Animals
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvAnimals.Columns[gvAnimals.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
                if (AlphabetFilter.Filter != AlphabetFilter.CLEAR_FILTER_KEY)
                {
                    e.InputParameters["filterExpression"] = "CommonName LIKE '" + AlphabetFilter.Filter + "%'";
                }
                else
                {
                    e.InputParameters["filterExpression"] = string.Empty;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("CommonName LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("ZooID LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("HouseName LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("ScientificName LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("AnimalRegion LIKE '%{0}%' OR ", freeText);
                sb.AppendFormat("RegionName LIKE '%{0}%' ", freeText);

                e.InputParameters["filterExpression"] = sb.ToString();
            }
            
      //      [AnimalRegionCode]
      //,[ZooID]
      //,[CommonName]
      //,[HouseName]
      //,[ScientificName]
      //,[Gender]
      //,[DOB]
      //,[CZArrival]
      //,[Active]
      //,[AnimalRegion]
      //,[RegionName]
      //,[AnimalRegionActive]

        }

        protected void gvAnimals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Animal animal = e.Row.DataItem as Animal;

                if (!animal.AnimalRegionActive)
                {
                    Label lblAnimalRegion = e.Row.FindControl("lblAnimalRegion") as Label;
                    lblAnimalRegion.CssClass = "inactive-record";
                    lblAnimalRegion.ToolTip = "This region is inactive!";
                }
            }           

        }

        protected void gvAnimals_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteAnimal" && base.IsMasterAdmin)
            {
                int animalId = Convert.ToInt32(e.CommandArgument);
                AnimalList.DeleteItem(animalId);
                this.Toast(PageExtensions.ToastMessageType.success, "Animal permanently deleted.", "Record Deleted");
            }

            gvAnimals.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int animalId = Convert.ToInt32(gvAnimals.DataKeys[row.RowIndex].Value);

            Animal animal = AnimalList.GetItem(animalId);
            animal.Active = cb.Checked;
            AnimalList.UpdateItem(animal);

            if (animal.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Animal activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Animal inactivated successfully.", "Record Updated");
            }           

            gvAnimals.DataBind();
            
        }

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            txtFreeText.Clear();
            gvAnimals.PageIndex = 0;
            gvAnimals.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAnimals.DataBind();
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            txtFreeText.Clear();
            gvAnimals.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvAnimals.DataBind();
        }
    }
}