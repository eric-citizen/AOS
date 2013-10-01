using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;
using KT.Extensions;

namespace CZAOSWeb.admin.master
{
    public partial class config : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.LoadData();

        }

        private void LoadData()
        {
            gvConfig.DataSource = CZBizObjects.SysCodeList.GetItemCollection();
            gvConfig.DataBind();
        }

        protected void gvConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteConfig" && base.IsMasterAdmin)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                SysCodeList.DeleteItem(id);
                this.Toast(PageExtensions.ToastMessageType.success, "Config permanently deleted.", "Record Deleted");

                this.LoadData();

            }

            
        }

        
    }
}