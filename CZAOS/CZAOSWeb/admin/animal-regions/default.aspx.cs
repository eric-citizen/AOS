using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;

namespace CZAOSWeb.admin.AnimalRegions
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!base.IsMasterAdmin)
                {
                    gvAnimalRegions.Columns[gvAnimalRegions.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
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
            
        }

        protected void gvAnimalRegions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvAnimalRegions_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteRegionCode" && base.IsMasterAdmin)
            {
                string animalRegionCode = Convert.ToString(e.CommandArgument);

                AnimalRegionList.DeleteItem(animalRegionCode);

                this.Toast(PageExtensions.ToastMessageType.success, "Region permananently deleted.", "Record Deleted");
            }

            gvAnimalRegions.DataBind();

        }

        protected void IsActiveCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            string arId = Convert.ToString(gvAnimalRegions.DataKeys[row.RowIndex].Value);

            AnimalRegion region = AnimalRegionList.GetItem(arId);
            region.Active = cb.Checked;
            AnimalRegionList.UpdateItem(region);

            if (region.Active)
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Region activated successfully.", "Record Updated");
            }
            else
            {
                this.Toast(PageExtensions.ToastMessageType.success, "Region inactivated successfully.", "Record Updated");
            }           

            gvAnimalRegions.DataBind();
            
        }
    }
}