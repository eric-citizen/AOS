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

namespace CZAOSWeb.admin.dialogs
{
    public partial class edit_config : MainBase 
    {

        private int SysCodeID
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
                if (!this.IsMasterAdmin)
                {
                    Dialog dialog = this.Master as Dialog;
                    dialog.RefreshParent();
                }
                else
                {
                    if (this.SysCodeID == 0)
                    {
                        //add new 
                        fieldsetLegend.Text = "Add";

                    }
                    else
                    {
                        fieldsetLegend.Text = "Edit";
                        this.LoadData();
                    }
                }
                
            }
        }

        private void LoadData()
        {
            SysCode item = SysCodeList.GetItem(this.SysCodeID);

            if (item == null)
            {
                this.DisplayError("SysCode ID {0} not found!".FormatWith(this.SysCodeID));
            }
            else
            {
                txtKey.Text = item.Description;
                txtValue.Text = item.Value;               

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                SysCode item = null;
                string key = txtKey.Text.Trim();
                string value = txtValue.Text.Trim();

                if (SysCodeList.KeyExists(this.SysCodeID, key))
                {
                    this.Toast(PageExtensions.ToastMessageType.error, "This key already exists for another record.", "Key Exists");
                    txtKey.Clear();
                    return;
                }

                if (this.SysCodeID == 0)
                {

                    item = new SysCode();
                    item.Description = key;
                    item.Value = value;

                    SysCodeList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "SysCode created successfully", "Record Created");                    

                }
                else    
                {
                    item = SysCodeList.GetItem(this.SysCodeID);

                    item.Description = key;
                    item.Value = value;

                    SysCodeList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "SysCode updated successfully", "Record Updated");

                   

                }

                txtKey.Clear();
                txtValue.Clear();

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }

    }
}