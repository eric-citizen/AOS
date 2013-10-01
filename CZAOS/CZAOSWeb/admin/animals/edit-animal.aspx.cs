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
    public partial class edit_animal : MainBase 
    {
        private int AnimalID
        {
            get
            {
                if (Request.QueryString.Contains("animalId"))
                {
                    string s = Request.QueryString["animalId"];
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

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.LoadRegions();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.AnimalID == 0)
                {
                    //add a new animal!
                    fieldsetLegend.Text = "Add New Animal";
                     
                }
                else
                {
                    fieldsetLegend.Text = "Edit Animal";
                    this.LoadData();
                }
            }
        }

        private void LoadData()
        {
            Animal animal = AnimalList.GetItem(this.AnimalID);

            if (animal == null)
            {
                this.DisplayError("Animal ID {0} not found!".FormatWith(this.AnimalID));
            }
            else
            {
                txtCommonName.Text = animal.CommonName;
                txtHouseName.Text = animal.HouseName;
                txtScientificName.Text = animal.ScientificName;
                ddlGender.SelectListItemByText(animal.Gender);
                txtZooID.Text = animal.ZooID;
                ddlRegion.SelectListItemByValue(animal.AnimalRegionCode);

                txtDOB.Text = animal.DOB;
                txtArrivalDate.Text = animal.CZArrival;
                chkActive.Checked = animal.Active;

            }

        }

        private void LoadRegions()
        {
            AnimalRegionList regions = AnimalRegionList.GetActiveItemCollection();
            ddlRegion.DataSource = regions;
            ddlRegion.DataBind();

            ddlRegion.AddEmptyListItem("Select Region", "-1");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Animal animal = null;

                PageMessageOptions options = new PageMessageOptions();
                options.OpaqueBg = false;
                options.UrlRedirect = "function:refreshParent()";

                if (this.AnimalID == 0)
                {
                    
                    animal = new Animal();
                    animal.CommonName = txtCommonName.HtmlEncodedText();
                    animal.HouseName = txtHouseName.HtmlEncodedText();
                    animal.ScientificName = txtScientificName.HtmlEncodedText();

                    if (ddlGender.SelectedIndex == 0)
                    {
                        animal.Gender = string.Empty;
                    }
                    else
                    {
                        animal.Gender = ddlGender.SelectedItem.Text;
                    }

                    animal.ZooID = txtZooID.Text;
                    animal.AnimalRegionCode = ddlRegion.SelectedValue;

                    animal.DOB = txtDOB.Text;
                    animal.CZArrival = txtArrivalDate.Text;

                    AnimalList.AddItem(animal);

                    this.Toast(PageExtensions.ToastMessageType.success, "Animal created successfully", "Record Created");                   

                }
                else
                {
                    animal = AnimalList.GetItem(this.AnimalID);

                    animal.CommonName = txtCommonName.HtmlEncodedText();
                    animal.HouseName = txtHouseName.HtmlEncodedText();
                    animal.ScientificName = txtScientificName.HtmlEncodedText();

                    if (ddlGender.SelectedIndex == 0)
                    {
                        animal.Gender = string.Empty;
                    }
                    else
                    {
                        animal.Gender = ddlGender.SelectedItem.Text;
                    }

                    animal.ZooID = txtZooID.Text;
                    animal.AnimalRegionCode = ddlRegion.SelectedValue;

                    animal.DOB = txtDOB.Text;
                    animal.CZArrival = txtArrivalDate.Text;

                    AnimalList.UpdateItem(animal);

                    this.Toast(PageExtensions.ToastMessageType.success, "Animal updated successfully", "Record Updated");                   

                }

            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
    }
}