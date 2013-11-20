using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;

namespace CZAOSWeb.controls
{
    public class PageChangeEventArgs : System.EventArgs
    {


        public PageChangeEventArgs()
        {
        }


        public PageChangeEventArgs(int newIndex, int inc)
        {
            mintNewPageIndex = newIndex;
            mintIncrement = inc;

        }

        private int mintNewPageIndex;

        private int mintIncrement;
        public int NewPageIndex
        {
            get { return mintNewPageIndex; }
            set { mintNewPageIndex = value; }
        }

        public int Increment
        {
            get { return mintIncrement; }
            set { mintIncrement = value; }
        }

    }

    public partial class GridPager : System.Web.UI.UserControl
    {

        public const string UNSELECTED_KEY = "-1";

        public delegate void PageChangeEventHandler(GridPager sender, PageChangeEventArgs args);
        //public event PageChangeEventHandler PageIndexChanged;
        public event EventHandler PageIndexChanged;

        private GridView mDataGrid;

        public Unit Width
        {
            get { return pagerPanel.Width; }
            set { pagerPanel.Width = value; }
        }

        public string CssClass
        {
            get { return pagerPanel.CssClass; }
            set { pagerPanel.CssClass = value; }
        }

        public string TextCssClass
        {
            get { return pageLabel.CssClass; }
            set
            {
                pageLabel.CssClass = value;
                pageJumpDropDownList.CssClass = value;
            }
        }

        public string GridViewID
        {
            get { return Convert.ToString(ViewState["GridViewID"]); }
            set { ViewState["GridViewID"] = value; }
        }

        public bool HideOnEmpty
        {
            get
            {
                if ((ViewState["HideOnEmpty"] == null))
                {
                    ViewState["HideOnEmpty"] = true;
                }
                return Convert.ToBoolean(ViewState["HideOnEmpty"]);
            }
            set { ViewState["HideOnEmpty"] = value; }
        }

        public bool ShowSinglePageTotal
        {
            get
            {
                if ((ViewState["ShowSinglePageTotal"] == null))
                {
                    ViewState["ShowSinglePageTotal"] = true;
                }
                return Convert.ToBoolean(ViewState["ShowSinglePageTotal"]);
            }
            set { ViewState["ShowSinglePageTotal"] = value; }
        }

        public bool LinqPaging
        {
            get
            {
                if ((ViewState["LinqPaging"] == null))
                {
                    ViewState["LinqPaging"] = false;
                }
                return Convert.ToBoolean(ViewState["LinqPaging"]);
            }
            set { ViewState["LinqPaging"] = value; }
        }

        public int Start
        {
            get { return Convert.ToInt32(ViewState["GridPager_Start"]); }
        }

        public int End
        {
            get { return Convert.ToInt32(ViewState["GridPager_End"]); }
        }

        public int Total
        {
            get
            {

                if ((mDataGrid.DataSource != null) && !this.LinqPaging)
                {
                    if (mDataGrid.DataSource is ICollection)
                    {
                        ICollection coll = (ICollection)mDataGrid.DataSource;
                        ViewState["GridPager_Total"] = coll.Count;

                    }
                    else if (mDataGrid.DataSource is DataTable)
                    {
                        DataTable dt = (DataTable)mDataGrid.DataSource;
                        ViewState["GridPager_Total"] = dt.Rows.Count;

                    }

                }
                else
                {
                    //'this is for grids using an objectdatasource.
                    //'this value must be set in the method that performs the GetCount function
                    //If IsNothing(ViewState["GridPager_Total"]) Then
                    //    ViewState["GridPager_Total"] = BizObjects.BranchList.RowCount ' HttpContext.Current.Items("rowCount"]
                    //End If

                }

                return Convert.ToInt32(ViewState["GridPager_Total"]);

            }
            set { ViewState["GridPager_Total"] = value; }
        }

        public int PageIndex
        {
            get { return mDataGrid.PageIndex; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.GridViewID))
            {
                mDataGrid = (GridView)this.NamingContainer.NamingContainer;

                if ((mDataGrid == null))
                {
                    throw new ArgumentNullException("GridPagerControl.GridViewControl", "GridView must be specified for GridPagerControl " + this.ID);
                }

            }
            else
            {                
                mDataGrid = (GridView)this.NamingContainer.FindControl(this.GridViewID);
            }

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((mDataGrid != null))
            {
                litTotalDiv.Visible = false;

                if (this.HideOnEmpty && this.Total <= mDataGrid.PageSize)
                {
                    pagerPanel.Visible = false;

                    if (this.ShowSinglePageTotal && !this.HideOnEmpty)
                    {
                        litTotal.Text = Convert.ToString((this.Total == 1 ? "Showing 1 Record" : string.Format("Showing {0} Records", this.Total)));
                        litTotalDiv.Visible = true;
                    }

                    return;

                }
                else
                {
                    pagerPanel.Visible = true;

                    this.CalculatePositionIndices(mDataGrid.PageSize, mDataGrid.PageIndex);
                    this.SetNextPreviousStates();

                    if (this.Total == 0)
                    {
                        pageLabel.Text = "No Records";
                        pageJumpDropDownList.SelectedIndex = 0;
                        pageJumpDropDownList.Enabled = false;

                    }
                    else
                    {
                        pageLabel.Text = string.Format("{0} - {1} of {2}", this.Start, this.End, this.Total);

                    }

                    this.LoadJumpDropDown();

                }

            }
            else
            {
                throw new ArgumentNullException("GridPagerControl.GridViewControl", "GridView must be specified for GridPagerControl " + this.ID);

            }
        }

        protected void pageJumpDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (pageJumpDropDownList.SelectedValue != UNSELECTED_KEY)
            {
                mDataGrid.PageIndex = Convert.ToInt32(pageJumpDropDownList.SelectedValue) - 1;
                if (PageIndexChanged != null)
                {
                    PageIndexChanged(this, new PageChangeEventArgs(mDataGrid.PageIndex, 0));
                }

            }
        }        

        protected void nextImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (mDataGrid.PageCount > mDataGrid.PageIndex)
            {
                mDataGrid.PageIndex += 1;
                if (PageIndexChanged != null)
                {
                    PageIndexChanged(this, new PageChangeEventArgs(mDataGrid.PageIndex, 1));
                }
            }

        }

        protected void previousImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (mDataGrid.PageIndex != 0)
            {
                mDataGrid.PageIndex -= 1;
                if (PageIndexChanged != null)
                {
                    PageIndexChanged(this, new PageChangeEventArgs(mDataGrid.PageIndex, -1));
                }
            }

        }

        private void SetNextPreviousStates()
        {
            if (mDataGrid.PageIndex <= 0)
            {
                previousImageButton.ImageUrl = "~/images/transparent.png";
            }
            else
            {
                previousImageButton.ImageUrl = "~/images/prev.gif";
            }

            if (mDataGrid.PageIndex >= (mDataGrid.PageCount - 1))
            {
                nextImageButton.ImageUrl = "~/images/transparent.png";
            }
            else
            {
                nextImageButton.ImageUrl = "~/images/next.gif";
            }

        }

        private void CalculatePositionIndices(int pageSize, int pageIndex)
        {

            ViewState["GridPager_Start"] = (pageSize * pageIndex) + 1;

            if (this.Start + (pageSize - 1) > this.Total)
            {
                ViewState["GridPager_End"] = this.Total;

            }
            else
            {
                ViewState["GridPager_End"] = this.Start + (pageSize - 1);
                // 9
            }

        }

        private void LoadJumpDropDown()
        {
            pageJumpDropDownList.Items.Clear();

            ListItem empty = new ListItem("Jump to", UNSELECTED_KEY);
            pageJumpDropDownList.Items.Add(empty);

            for (int intIndex = 1; intIndex <= mDataGrid.PageCount; intIndex++)
            {
                ListItem item = new ListItem(string.Format("Page {0} of {1}", intIndex.ToString(), mDataGrid.PageCount), intIndex.ToString());
                pageJumpDropDownList.Items.Add(item);

            }

            pageJumpDropDownList.SelectListItemByValue((mDataGrid.PageIndex + 1).ToString());

        }
    }
}