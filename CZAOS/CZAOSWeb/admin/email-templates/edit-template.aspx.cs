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
    public partial class edit_template : MainBase 
    {

        private int TemplateID
        {
            get
            {
                if (Request.QueryString.Contains("etId"))
                {
                    hdnItemID.Value = Request.QueryString["etId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!this.IsPostBack)
            {
                if (this.TemplateID == 0)
                {
                    //add new 
                    fieldsetLegend.Text = "Add New Template";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Template";
                    this.LoadData();
                }
            }
        }        

        private void LoadData()
        {
            EmailTemplate item = EmailTemplateList.GetItem(this.TemplateID);

            if (item == null)
            {
                this.DisplayError("Template ID {0} not found!".FormatWith(this.TemplateID));
            }
            else
            {
                txtKey.Text = item.Key;
                txtInstructions.Text = item.InstructionalText;
                txtSubject.Text = item.Subject;
                //html editor = item.Body;
                chkActive.Checked = item.Active;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                EmailTemplate item = null;

                if (this.TemplateID == 0)
                {
                    item = new CZDataObjects.EmailTemplate();
                    item.Key = txtKey.Text;
                    item.Subject = txtSubject.Text;
                    item.InstructionalText = txtInstructions.Text;
                    //item.Body = //html editor 
                    item.Active = chkActive.Checked;

                    EmailTemplateList.AddItem(item);
                    
                    this.Toast(PageExtensions.ToastMessageType.success, "Template created successfully", "Record Created");

                    //Dialog dialog = this.Master as Dialog;
                    //dialog.RefreshParent();

                }
                else    
                {
                    item = EmailTemplateList.GetItem(this.TemplateID);

                    item.Key = txtKey.Text;
                    item.InstructionalText = txtInstructions.Text;
                    item.Subject = txtSubject.Text;
                    //item.Body = //html editor 
                    item.Active = chkActive.Checked;

                    EmailTemplateList.UpdateItem(item);
                   
                    this.Toast(PageExtensions.ToastMessageType.success, "Template updated successfully", "Record Updated");                    

                }             

                

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}