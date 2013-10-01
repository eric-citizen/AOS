﻿using System;
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
using System.Web.Script.Serialization;
using System.Web.Services;

namespace CZAOSWeb.admin.dialogs
{
    public partial class sort_behavior : MainBase 
    {

        private int CategoryID
        {
            get
            {
                if (Request.QueryString.Contains("bcatId"))
                {
                    hdnCategoryID.Value = Request.QueryString["bcatId"];
                }

                return hdnCategoryID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.CategoryID == 0)
            {
                Dialog dialog = this.Master as Dialog;
                dialog.RefreshParent();
                return;
            }

            if (!this.IsPostBack)
            {
                this.LoadData();
            }
        }

        private void LoadData()
        {
            rptSortItems.DataSource = BehaviorList.GetItemCollection(0, 0, "SortOrder ASC", "BvrCatID = " + this.CategoryID);
            rptSortItems.DataBind();
        }

        [WebMethod]
        public static void SaveSortOrder(string bcatId, string sortedIds)
        {
            if (bcatId.IsNullOrEmpty() || sortedIds.IsNullOrEmpty())
                return;


            //expect this Format:
            //bodyContent_rptAltImages_liAltImg_2,bodyContent_rptAltImages_liAltImg_1,bodyContent_rptAltImages_liAltImg_3,bodyContent_rptAltImages_liAltImg_0 
            List<string> ids = new List<string>(sortedIds.Split(','));
            List<int> newOrder = new List<int>();

            foreach (string id in ids)
            {
                if (id.IsNotNullOrEmpty())
                {
                    int index = id.LastIndexOf('_');

                    if (index > -1)
                    {
                        string indexer = id.Substring(index + 1);
                        if (indexer.IsNumeric())
                        {
                            newOrder.Add(indexer.ToInt32());
                        }
                    }

                }
            }

            if (newOrder.Count > 0)
            {
                BehaviorList items = BehaviorList.GetItemCollection(0, 0, "SortOrder ASC", "BvrCatID = " + bcatId.ToInt32());
                List<CZDataObjects.Interfaces.ISortable> sortList = new List<CZDataObjects.Interfaces.ISortable>();

                int tindex = 1;
                foreach (int i in newOrder)
                {
                    items[i].SortOrder = tindex;
                    sortList.Add(items[i]);

                    tindex++;
                }

                BehaviorList.UpdateSort(sortList);

                
            }

        }

        protected void rptSortItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

    }
}