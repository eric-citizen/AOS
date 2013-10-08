using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CZBizObjects;
using CZDataObjects;
using KT.Extensions;

namespace CZAOSWeb.controls
{
    public partial class GroupControl : System.Web.UI.UserControl, IValidator
    {
        public string Count
        {
            get
            {                
                return litID.Text;
            }

            set
            {
                 litID.Text = value;
            }
        }

        public string GroupName
        {
            get
            {
                return txtGroupName.Text;
            }
            set
            {
                txtGroupName.Text = value;
            }

        }

        public bool GroupNameVisible
        {
            get
            {
                return txtGroupName.Visible;
            }
            set
            {
                txtGroupName.Visible = value;
            }

        }

        public List<int> SelectedAnimalIDs
        {
            get
            {
                List<int> ids = new List<int>();
                foreach (ListItem item in lstAnimals.Items)
                {
                    if (item.Selected)
                        ids.Add(item.Value.ToInt32());
                }

                return ids;
            }
            set
            {
                List<int> ids = value;

                foreach (ListItem item in lstAnimals.Items)
                {
                    if (ids.Contains(item.Value.ToInt32()))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }
        }

        public string AnimalRegionCode
        {
            get {
                return Convert.ToString(ViewState["AnimalRegionCode"]);
            }
            set {
                ViewState["AnimalRegionCode"] = value;
            }

        }

        public string ErrorMessage
        {
            get
            {                
                return "Too many animals selected for group {0} - {1}".FormatWith(this.GroupName, this.SelectedCount);
            }
            set
            {
                //stub
            }
        }

        public bool IsValid
        {
            get;
            set;
        }

        public void Validate()
        {
            SysCode config = SysCodeList.Get("maxanimalgroupcount");
            int max = config.Value.ToInt32();
            this.IsValid = (this.SelectedCount <= max);
        }

        public int SelectedCount
        {
            get
            {
                int count = 0;
                foreach (ListItem item in lstAnimals.Items)
                {
                    if (item.Selected)
                        count++;
                }

                return count;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Validators.Add(this);

            if (this.IsPostBack)
            {
                this.LoadGroup(this.AnimalRegionCode);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            if ((Page != null))
            {
                Page.Validators.Remove(this);
            }
            base.OnUnload(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SysCode config = SysCodeList.Get("maxanimalgroupcount");
            int max = config.Value.ToInt32();

            litMax.Text = max.ToString();
        }

        public void DeselectLastOption()
        {
            lstAnimals.Items[lstAnimals.SelectedIndex].Selected = false;
        }

        public void LoadGroup(string animalRegionCode)
        {
            this.AnimalRegionCode = animalRegionCode;

            AnimalList list = AnimalList.GetActiveAnimals(animalRegionCode);

            lstAnimals.DataSource = list;
            lstAnimals.DataBind();

        }

        //protected void lstAnimals_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selected = this.SelectedCount;
        //    SysCode config = SysCodeList.Get("maxanimalgroupcount");
        //    int max = config.Value.ToInt32();

        //    litMax.Text = max.ToString();

        //    if (selected <= max)
        //    {
        //        litCount.Text = selected.ToString();
        //    }
        //    else
        //    {
        //        lstAnimals.Items[lstAnimals.SelectedIndex].Selected = false;
        //    }            
            
        //}

        public void Clear()
        {
            lstAnimals.Items.Clear();
            //foreach (ListItem item in lstAnimals.Items)
            //{
            //    item.Selected = false;
            //}
        }
    }
}