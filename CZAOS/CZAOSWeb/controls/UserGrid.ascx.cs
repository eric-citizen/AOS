using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KT.WebControls;
using System.Web.Security;
using CZAOSCore.basepages;
using CZAOSMail.Mail;
using KT.Extensions;

namespace CZAOSWeb.controls
{
    public partial class UserGrid : System.Web.UI.UserControl
    {
        private enum DataColumns
        {
            Username,
            DisplayName,
            Email,
            UserType,
            //Expiration,
            Unlock,
<<<<<<< HEAD
            SendPassword,  
            EditUser,
=======
            //LastActivityDate,
            SendPassword,            
>>>>>>> feb1721df6f3a023e0ce9c9ad679138330750b91
            DeleteUser      
        }

        public string UserType
        {
            get
            {
                if (ViewState["UserType"] == null)
                {
                    ViewState["UserType"] = string.Empty;
                }

                return Convert.ToString(ViewState["UserType"]);
            }
            set
            {
                ViewState["UserType"] = value;                
            }
        }

        public int Count
        {
            get { return userGridView.Rows.Count; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MainBase page = this.Page as MainBase;
            if (!page.IsMasterAdmin)
            {
                userGridView.Columns[userGridView.Columns.Count - 1].Visible = false; //hide delete column from all but master admins
            }
        }

       

        protected void AlphabetFilter_AlphabetSelected(object sender, EventArgs e)
        {
            userGridView.PageIndex = 0;
            userGridView.DataBind();
        }

        protected void userGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MainBase page = this.Page as MainBase;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                MembershipUser user = (MembershipUser)e.Row.DataItem;
                CZDataObjects.CZUser czuser = CZBizObjects.UserList.GetUser(user.UserName);

                Literal litDName = e.Row.FindControl("litDName") as Literal;
                Literal litUserType = e.Row.FindControl("litUserType") as Literal;
                Literal litExpDate = e.Row.FindControl("litExpDate") as Literal;

                litDName.Text = czuser.DisplayName;
                litUserType.Text = czuser.UserType;

                if(czuser.ExpirationDate.HasValue)
                {
                    DateTime ed = (DateTime)czuser.ExpirationDate;
                    //litExpDate.Text = ed.ToString("MM/dd/yyy");
                }


                if (page.IsMasterAdmin)
                {
                    if (user.IsLockedOut)
                    {
                        e.Row.Cells[(int)DataColumns.Username].ToolTip = "User is locked out!";
                    }
                    else
                    {
                        e.Row.Cells[(int)DataColumns.Username].ToolTip = user.GetPassword();
                    }

                }

                if (!user.IsLockedOut)
                {
                    e.Row.Cells[(int)DataColumns.Unlock].Text = "";

                }
                else
                {
                    LinkButton sendPwdLink = (LinkButton)e.Row.Cells[(int)DataColumns.SendPassword].Controls[0];
                    sendPwdLink.Enabled = false;
                    sendPwdLink.ToolTip = "Cannot send password when user account is locked";
                }                

                if (user.IsOnline)
                {
                    e.Row.Cells[(int)DataColumns.Username].CssClass = "parent-row-highlight";
                    //e.Row.Cells[(int)DataColumns.LastActivityDate].ToolTip = "This user is currently online";
                }

            }

        }

        protected void userGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MainBase p = (MainBase)this.Page;

            if (e.CommandName == "SendPassword")
            {
                string strUserName = e.CommandArgument.ToString();

                Notifications mailer = new Notifications();
                mailer.SendUserCredentials(strUserName, this);
                
                PageMessageOptions options = new PageMessageOptions();
                options.FadeOut = true;

                if (mailer.LastError.IsNotNullOrEmpty())
                {
                    p.DisplayError("Unable to send email: " + mailer.LastErrorTitle);
                }
                else
                {                    
                    p.Toast("Login credentials sent successfully", "");
                }



            }
            else if (e.CommandName == "UnlockUser")
            {
                string strUserName = this.GetUsername(e);
                MembershipUser editUser = default(MembershipUser);

                editUser = Membership.GetUser(strUserName);

                if (editUser.IsLockedOut)
                {
                    editUser.UnlockUser();
                    p.Toast("User unlocked", "");
                }


            }
            else if (e.CommandName == "DeleteUser")
            {
                string userId = Convert.ToString(e.CommandArgument);
                Guid id = new Guid(userId);
                MembershipUser editUser = default(MembershipUser);
                CZDataObjects.CZUser user = CZBizObjects.UserList.GetItem(new Guid(userId));

                editUser = Membership.GetUser(id);
                user.Active = false;

                CZBizObjects.UserList.UpdateItem(user);

                editUser.IsApproved = false;
                Membership.UpdateUser(editUser);
                p.Toast("User inactivated successfully.", "");
            } 

            userGridView.DataBind();

        }

        protected void userDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is int)
            {
                userGridViewPagerControl.Total = Convert.ToInt32(e.ReturnValue);
            }
        }

        protected void userDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["pageIndex"] = userGridView.PageIndex;
            e.InputParameters["pageSize"] = userGridView.PageSize;
            e.InputParameters["role"] = this.UserType;

            if (AlphabetFilter.Filter != AlphabetFilter.CLEAR_FILTER_KEY)
            {
                e.InputParameters["startsWith"] = AlphabetFilter.Filter;
            }
            else
            {
                e.InputParameters["startsWith"] = string.Empty;
            }
        }

        private string GetUsername(System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int intIndex = Convert.ToInt32(e.CommandArgument);
            return Convert.ToString(userGridView.DataKeys[intIndex].Values[0]);
        }

        protected void userGridView_PreRender(object sender, EventArgs e)
        {

        }
    }
}