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

namespace CZAOSWeb.admin.nav
{
    public partial class edit_nav : MainBase 
    {
        private int NavID
        {
            get
            {
                if (Request.QueryString.Contains("navId"))
                {
                    hdnItemID.Value = Request.QueryString["navId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.NavID == 0)
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

        private void LoadData()
        {
            AdminNavigation item = AdminNavigationList.GetItem(this.NavID);

            if (item == null)
            {
                this.DisplayError("Nav ID {0} not found!".FormatWith(this.NavID));
            }
            else
            {
                txtFolder.Text = item.Folder;
                txtNavText.Text = item.NavText;

                foreach (string role in item.RoleList)
                {
                    foreach (ListItem li in cbxRoles.Items)
                    {
                        if (li.Value.Equals(role))
                        {
                            li.Selected = true;
                        }
                    }
                }
               

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                AdminNavigation item = null;

                if (this.NavID == 0)
                {
                    item = new AdminNavigation();

                    item.Folder = txtFolder.HtmlEncodedText().Trim();
                    item.NavText = txtNavText.HtmlEncodedText().Trim();

                    foreach (ListItem li in cbxRoles.Items)
                    {
                        if (li.Selected)
                        {
                            item.AddRole(li.Value);
                        }
                    }

                    AdminNavigationList.AddItem(item);

                    this.Toast(PageExtensions.ToastMessageType.success, "Nav item created successfully", "Record Created");


                }
                else    
                {
                    item = AdminNavigationList.GetItem(this.NavID);

                    item.Folder = txtFolder.HtmlEncodedText().Trim();
                    item.NavText = txtNavText.HtmlEncodedText().Trim();
                    item.Roles = string.Empty;

                    foreach (ListItem li in cbxRoles.Items)
                    {
                        if (li.Selected)
                        {
                            item.AddRole(li.Value);
                        }
                    }

                    AdminNavigationList.UpdateItem(item);

                    //this.DisplayMessage("Region updated successfully.", options);
                    this.Toast(PageExtensions.ToastMessageType.success, "Nav item updated successfully", "Record Updated");


                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}