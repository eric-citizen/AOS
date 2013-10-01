using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CZBizObjects;
using CZDataObjects;
using KT.Extensions;
using CZAOSCore;

namespace CZAOSWeb.controls
{
    public partial class AdminNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminNavigationList myNav = AdminNavigationList.GetNavigationForUser();
            itemCount = myNav.Count;
            rptMenu.DataSource = myNav;
            rptMenu.DataBind();
        }

        private int navIndex = 0;
        private int itemCount = 0;

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.IsDataItem())
            {
                AdminNavigation nav = e.Item.DataItem as AdminNavigation;
                HyperLink lnkNav = e.FindControl<HyperLink>("lnkNav");

                if (navIndex == 0)
                {
                    lnkNav.CssClass = "first";
                }

                if (navIndex == itemCount)
                {
                    lnkNav.CssClass = "last";
                }

                string folder = RequestProperties.Current.StrippedVirtualPath.RemoveString("admin/").RemoveString("/");
                if (nav.Folder.Equals(folder))
                {
                    lnkNav.CssClass += " selected";
                }

                navIndex++;
            }
            

        }

        protected void rptMenu_PreRender(object sender, EventArgs e)
        {

        }
    }
}