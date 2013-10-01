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

namespace CZAOSWeb.admin.changes
{
    public partial class view_changes : MainBase 
    {

        //Object Type: Wind, ID: 8

        private int ItemID
        {
            get
            {
                if (Request.QueryString.Contains("id"))
                {
                    hdnItemID.Value = Request.QueryString["id"]; 
                }

                return hdnItemID.ItemID;
            }
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.ItemID == 0)
                {
                    this.DisplayError("ItemID/ObjectTypeName missing");
                    Dialog dialog = this.Master as Dialog;
                    dialog.RefreshParent();
                     
                }
                else
                {                    
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            ChangeLog item = ChangeLogList.GetItem(this.ItemID);            

            if (item == null)
            {
                this.DisplayError("Log ID {0} not found!".FormatWith(this.ItemID));
            }
            else
            {
                litTable.Text = item.Identifier;
                litName.Text = item.UserDisplayName;
                litDate.Text = item.ChangeDate.ToString("f");
                litType.Text = item.ChangeType.ToString();

                gvChanges.DataSource = item.ChangeItemList;
                gvChanges.DataBind();
            }

        }
    }
}