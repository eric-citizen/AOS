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
    public partial class view_tracking : MainBase 
    {

        private int TrackingID
        {
            get
            {
                if (Request.QueryString.Contains("tId"))
                {
                    hdnItemID.Value = Request.QueryString["tId"]; 
                }

                return hdnItemID.ItemID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!this.IsPostBack)
            {
                if (this.TrackingID == 0)
                {
                    this.DisplayError("Tracking item not found");
                }
                else
                {                    
                    this.LoadData();
                }
            }
        }        

        private void LoadData()
        {
            EmailTracking item = EmailTrackingList.GetItem(this.TrackingID);

            if (item == null)
            {
                this.DisplayError("Tracking ID {0} not found!".FormatWith(this.TrackingID));
            }
            else
            {
                litSent.Text = item.SendDate.ToString("f");
                litTo.Text = item.To;
                litFrom.Text = item.From;

                if(item.UserID.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    litUser.Text = "System";
                }
                else
                {
                    CZUser user = CZBizObjects.UserList.GetUserByID(item.UserID.ToString());
                    litUser.Text = user.DisplayName;
                }

                if(item.Sent)
                {
                    litSendOK.Text = "Yes";

                    if (item.Opened)
                    {
                        litOpened.Text = "Yes, on {0}".FormatWith(item.OpenDate.ToString("f"));
                    }
                    else
                    {
                        litOpened.Text = "Not yet";
                    }
                }
                else
                {
                    litSendOK.Text = "No - {0}".FormatWith(item.FailReason);
                    litOpened.Text = string.Empty;
                }
 
                
                
                litSubject.Text = item.Subject;
                litBody.Text = item.Body;
            }

        }

    }
}