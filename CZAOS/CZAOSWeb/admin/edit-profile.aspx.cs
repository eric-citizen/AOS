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
using CZAOSCore.Enums;

namespace CZAOSWeb.admin.users
{
    public partial class edit_profile : MainBase 
    {
        private string UserID
        {
            get
            {
                return Membership.GetUser().ProviderUserKey.ToString();
            }
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {                
                this.SetupPasswordControls(false);
                this.LoadUserData();               
            }
        }

        private void LoadUserData()
        {
            if (this.UserID.IsNotNullOrEmpty())
            {
                CZUser user = CZBizObjects.UserList.GetItem(new Guid(this.UserID));

                if (user == null)
                {
                    this.DisplayError("User ID {0} not found!".FormatWith(this.UserID));
                }
                else
                {                           

                    txtDisplayName.Text = user.DisplayName;                    
                    chkNewEmail.Checked = user.NewEmail;
                    chkCompEmail.Checked = user.CompEmail;
                    chkObserveEmail.Checked = user.ObserveEmail;
                    chkWeekEmail.Checked = user.WeekEmail;                   

                    if (user.UserType.IsNotNullOrEmpty())
                    {
                        regionPanel.Visible = true;
                        divMessage.Visible = false;                                           
                    }
                    else
                    {
                        divMessage.Text = "Animal regions do not need to be assigned for this user type.";
                        regionPanel.Visible = false;
                    }                    

                }

                this.LoadMembershipData();
            }
        }

        private void SetupPasswordControls(bool active)
        {
            txtNewPassword.Required = active;
            txtConfirmPassword.Required = active;               
        }

        private void LoadMembershipData()
        {
            if (this.UserID.IsNotNullOrEmpty())
            {
                MembershipUser member = Membership.GetUser(new Guid(this.UserID));

                if ((member != null))
                {                    
                    txtEmail.Text = member.Email;                       
                    
                }
            }
            
        }         

        private void LoadUserRegionData()
        {
            if (this.UserID.IsNullOrEmpty())
                return;

            CZUser user = UserList.GetUserByID(this.UserID);
            UserRegionList list = UserRegionList.GetActive(user.Username);
            dlRegions.DataSource = list;
            dlRegions.DataBind();
            
        }

        protected void regexPassword_Init(object sender, EventArgs e)
        {
            regexPassword.ValidationExpression = Membership.PasswordStrengthRegularExpression;
            regexPassword2.ValidationExpression = regexPassword.ValidationExpression;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                bool ok = false;
                
                ok = this.UpdateUser();

                if (ok)
                {
                    this.Toast("You have updated your profile");
                }
               
            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }
       
        private bool UpdateUser()
        {
            MembershipUser editUser = default(MembershipUser);

            editUser = Membership.GetUser();
            editUser.Email = txtEmail.Text;            

            try
            {
                Membership.UpdateUser(editUser);              

            }
            catch (Exception ex)
            {               
                this.DisplayError("Error updating user: " + ex.Message);
                return false;

            }

            bool pwdchanged = false;
            if (txtConfirmPassword.Text.Length > 0 && this.PasswordIsValid())
            {
                editUser.ChangePassword(editUser.GetPassword(), txtConfirmPassword.Text);
                pwdchanged = true;
            }           

            CZUser user = CZBizObjects.UserList.GetItem(new Guid(this.UserID));

            user.DisplayName = txtDisplayName.Text;

            if (pwdchanged)
                user.ExpirationDate = System.DateTime.Now.AddDays(90);

            user.NewEmail = chkNewEmail.Checked;
            user.CompEmail = chkCompEmail.Checked;
            user.ObserveEmail = chkObserveEmail.Checked;
            user.WeekEmail = chkWeekEmail.Checked;

            CZBizObjects.UserList.UpdateItem(user);            

            return true;

        }
        
        private bool PasswordIsValid()
        {
            if (this.UserID.IsNullOrEmpty())
            {
                return txtNewPassword.IsValid && txtConfirmPassword.IsValid && pwdCompare.IsValid && regexPassword.IsValid && regexPassword2.IsValid;
            }
            else
            {
                if (txtNewPassword.Text.IsNotNullOrEmpty() && txtConfirmPassword.Text.IsNotNullOrEmpty())
                {
                    return txtNewPassword.IsValid && txtConfirmPassword.IsValid && pwdCompare.IsValid && regexPassword.IsValid && regexPassword2.IsValid;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}