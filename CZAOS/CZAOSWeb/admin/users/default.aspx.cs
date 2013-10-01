using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.Extensions;
using CZBizObjects;
using CZDataObjects;
using CZAOSCore.basepages;

namespace CZAOSWeb.admin.users
{
    public partial class _default : MainBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!this.IsMasterAdmin)
                {
                    this.RemoveTab(3);
                }
                
            }
        }
    }
}