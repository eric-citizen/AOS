using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;

namespace CZAOSWeb.controls
{

    public class AlphabetFilterArgs : System.EventArgs
    {
        public AlphabetFilterArgs(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                mstrFilter = filter;
            }
            else
            {
                mstrFilter = string.Empty;
            }

        }


        private string mstrFilter;
        public string Filter
        {
            get { return mstrFilter; }
        }

    }

    public partial class AlphabetFilter : System.Web.UI.UserControl
    {
        public event EventHandler AlphabetSelected;

        private void OnAlphabetSelected(string selection)
        {
            AlphabetFilterArgs args = new AlphabetFilterArgs(selection);

            if (AlphabetSelected != null)
            {
                AlphabetSelected(this, args);
            }

        }

        public const string CLEAR_FILTER_KEY = "All";

        private const string VIEWSTATE_KEY = "AlphabetFilterControl_Filter";
        public string Caption
        {
            get { return captionLabel.Text; }
            set { captionLabel.Text = value; }
        }

        public string CssClass
        {
            get { return repeaterDivPanel.CssClass; }
            set
            {
                repeaterDivPanel.CssClass = value;
                captionLabel.CssClass = value;
            }
        }

        public bool AlphaNumeric
        {
            get
            {
                if ((ViewState["AlphaNumeric"] == null))
                {
                    ViewState["AlphaNumeric"] = false;
                }
                return Convert.ToBoolean(ViewState["AlphaNumeric"]);
            }
            set { ViewState["AlphaNumeric"] = value; }
        }       

        public string Filter
        {
            get
            {
                if ((ViewState[VIEWSTATE_KEY] == null))
                {
                    return CLEAR_FILTER_KEY;
                }
                else
                {
                    return Convert.ToString(ViewState[VIEWSTATE_KEY]);
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadAlphabet();
            }
        }

        private void LoadAlphabet()
        {
            string[] strAlphabet = null;

            if (this.AlphaNumeric)
            {
                strAlphabet = "A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;0;1;2;3;4;5;6;7;8;9;All".Split(';');
            }
            else
            {
                strAlphabet = "A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;All".Split(';');
            }

            findRepeater.DataSource = strAlphabet;
            findRepeater.DataBind();
        }

        public void Clear()
        {

            foreach (RepeaterItem item in findRepeater.Items)
            {
                LinkButton link = (LinkButton)item.FindControl("nameLink");
                link.Font.Bold = false;

            }

            ViewState[VIEWSTATE_KEY] = CLEAR_FILTER_KEY;            

        }

        protected void findRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton nameLink = (LinkButton)e.Item.FindControl("nameLink");

                if (this.Filter.ToLower() == nameLink.Text.ToLower())
                {
                    if (!nameLink.CssClass.Contains(" alphabet-selected"))
                        nameLink.CssClass += " alphabet-selected";
                }
                else
                {
                    nameLink.CssClass = nameLink.CssClass.Replace("alphabet-selected","");
                }

            }
        }

        protected void findRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState[VIEWSTATE_KEY] = e.CommandArgument.ToString();

            foreach (RepeaterItem item in findRepeater.Items)
            {
                LinkButton link = (LinkButton)item.FindControl("nameLink");
                link.Font.Bold = false;
                link.CssClass = "alphabet-letter";
            }

            LinkButton nameLink = (LinkButton)e.Item.FindControl("nameLink");
            nameLink.CssClass += " alphabet-selected";

            this.OnAlphabetSelected(e.CommandArgument.ToString());

            //AlphabetFilterArgs args = new AlphabetFilterArgs(e.CommandArgument.ToString());

            //if (FilterSelected != null)
            //{
            //    FilterSelected(this, args);
            //}
        }
    }
}