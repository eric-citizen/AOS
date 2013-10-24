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
    public partial class edit_user : MainBase 
    {       

        private string UserID
        {
            get
            {
                if (Request.QueryString.Contains("UserId"))
                {
                    hdnUserID.Value = Request.QueryString["Userid"]; 
                }

                return hdnUserID.Value;
            }
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            dteExpiration.DateValue = System.DateTime.Now.AddDays(90);
            dteExpiration.ReadOnly = true;

            this.LoadRoles();
            this.LoadUserTypes();

            if (UserID.Length > 0)
                txtUsername.ReadOnly = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadRegionData();

                if (this.UserID.IsNullOrEmpty())
                {
                    //add a new user!
                    fieldsetLegend.Text = "Add New User";
                    this.SetupPasswordControls(true);
                    this.LoadMembershipData();
                    regionPanel.Visible = false;
                }
                else
                {                    
                    fieldsetLegend.Text = "Edit User";
                    this.SetupPasswordControls(false);
                    this.LoadUserData();
                }
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
                    txtUsername.Text = user.Username;                   

                    txtDisplayName.Text = user.DisplayName;
                    dteExpiration.DateValue = (DateTime)user.ExpirationDate;
                    chkNewEmail.Checked = user.NewEmail;
                    chkCompEmail.Checked = user.CompEmail;
                    chkObserveEmail.Checked = user.ObserveEmail;
                    chkWeekEmail.Checked = user.WeekEmail;
                    chkActive.Checked = user.Active;

                    if (user.UserType.IsNotNullOrEmpty())
                    {
                        ddlUserType.SelectListItemByText(user.UserType);
                    }

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
                    txtUsername.Text = member.UserName;
                    txtEmail.Text = member.Email;
                    txtComments.Text = member.Comment;

                    string[] roles = Roles.GetRolesForUser(member.UserName);

                    if (roles.Length > 0)
                    {
                        ddlAccess.SelectListItemByText(roles[0]);

                        if (roles[0] == CoreUserTypeRoles.MasterAdmin.ToString())
                            ddlAccess.Enabled = false;

                    }
                    
                }
            }
            
        }

        private void LoadRoles()
        {
            ddlAccess.DataSource = Roles.GetAllRoles();
            ddlAccess.DataBind();

            //<asp:ListItem Selected="True" Text="Select a user type" Value="-1"></asp:ListItem>    
            ddlAccess.AddEmptyListItem("Select an access level", "-1");

        }

        private void LoadUserTypes()
        {
            ddlUserType.DataSource = Helpers.GetCoreUserTypes();
            ddlUserType.DataBind();

            ddlUserType.AddEmptyListItem("Select a user type", "-1");

        }

        private void LoadRegionData()
        {
            AnimalRegionList list = AnimalRegionList.GetActiveItemCollection();

            if (list == null || list.Count == 0)
            {
                this.DisplayError("Did not find any animal regions!!");
            }
            else
            {
                cbxRegions.DataSource = list;
                cbxRegions.DataBind();
                this.LoadUserRegionData();
            }

        }

        private void LoadUserRegionData()
        {
            if (this.UserID.IsNullOrEmpty())
                return;

            CZUser user = UserList.GetUserByID(this.UserID);
            UserRegionList list = UserRegionList.GetActive(user.Username);

            foreach (ListItem li in cbxRegions.Items)
            {
                foreach (UserRegion region in list)
                {
                    if (li.Value.Equals(region.AnimalRegionCode))
                    {
                        li.Selected = true;
                    }                    
                }
            }
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
                if (this.UserID.IsNullOrEmpty())
                {
                    ok = this.AddNewUser();
                    if (ok)
                    {
                        PageMessageOptions options = new PageMessageOptions();
                        options.UrlRedirect = "function:CZAOSUIDialogs.refreshParent()";

                        this.DisplayMessage("User created successfully.", options);
                    }
                }
                else
                {
                    ok = this.UpdateUser();

                    if (ok)
                    {
                        PageMessageOptions options = new PageMessageOptions();
                        options.UrlRedirect = "function:CZAOSUIDialogs.refreshParent()";

                        this.DisplayMessage("User updated successfully.", options);
                    }
                }
            }
            else
            {
                this.DisplayError("There are one or more data errors. Please correct and try again.");
            }
        }

        private bool AddNewUser()
        {
            string emailTest = Membership.GetUserNameByEmail(txtEmail.Text);

            if (!string.IsNullOrEmpty(emailTest))
            {                
                this.DisplayError("The specified email address is already in use with another user.");
                return false;
            }

            MembershipUser newUser = Membership.GetUser(txtUsername.Text);

            if ((newUser == null))
            {
                MembershipCreateStatus status = MembershipCreateStatus.UserRejected;

                try
                {
                    newUser = Membership.CreateUser(txtUsername.Text, txtNewPassword.Text, txtEmail.Text, null, null, true, out status);
                }
                catch (Exception ex)
                {
                    this.DisplayError("Error creating user: " + ex.Message);
                    return false;
                }

                if ((newUser == null))
                {
                    this.DisplayError("Error creating user: " + this.GetErrorMessage(status));
                    return false;
                }

                if (txtComments.Text.Length > 0)
                {
                    newUser.Comment = txtComments.Text;
                }

                Membership.UpdateUser(newUser);
                Roles.AddUserToRole(newUser.UserName, ddlAccess.SelectedItem.Text);          
                

                //3) Create Profile
                //this.SaveProfileData(this.AddNew, newUser.UserName, newUser.ProviderUserKey.ToString);
                CZUser user = new CZUser();

                user.Username = newUser.UserName;
                user.UserId = (Guid)newUser.ProviderUserKey;
                user.DisplayName = txtDisplayName.Text;
                user.ExpirationDate = dteExpiration.DateValue;
                user.NewEmail = chkNewEmail.Checked;
                user.CompEmail = chkCompEmail.Checked;
                user.ObserveEmail = chkObserveEmail.Checked;
                user.WeekEmail = chkWeekEmail.Checked;
                user.Active = chkActive.Checked;

                if (ddlAccess.SelectedItem.Text != "MasterAdmin")
                {
                    user.UserType = ddlUserType.SelectedItem.Text;
                }
                else
                {
                    user.UserType = string.Empty;
                }
                

                CZUser newguy = CZBizObjects.UserList.AddItem(user);
                
            }
            else
            {                
                this.DisplayError(string.Format("This username is already in use: user {0}, email: {1}", newUser.UserName, newUser.Email));
                return false;
            }

            return true;

        }

        private bool UpdateUser()
        {
            MembershipUser editUser = default(MembershipUser);

            editUser = Membership.GetUser(txtUsername.Text);
            editUser.Email = txtEmail.Text;
            editUser.Comment = txtComments.Text;
            editUser.IsApproved = chkActive.Checked;

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

            //TODO
            //RolesControl1.SaveRoles(editUser.UserName);

            CZUser user = CZBizObjects.UserList.GetItem(new Guid(this.UserID));

            if (!Roles.IsUserInRole(editUser.UserName, ddlAccess.SelectedItem.Text))
            {
                string[] roles = Roles.GetRolesForUser(editUser.UserName);

                foreach (string role in roles)
                {
                    Roles.RemoveUserFromRole(editUser.UserName, role);
                }

                Roles.AddUserToRole(editUser.UserName, ddlAccess.SelectedItem.Text);          
            }
            

            if (user == null) //debug only
            {
                user = new CZUser();

                user.Username = txtUsername.Text;
                user.UserId = (Guid)editUser.ProviderUserKey;
                user.DisplayName = txtDisplayName.Text;

                if (pwdchanged)
                    user.ExpirationDate = System.DateTime.Now.AddDays(90);

                user.NewEmail = chkNewEmail.Checked;
                user.CompEmail = chkCompEmail.Checked;
                user.ObserveEmail = chkObserveEmail.Checked;
                user.WeekEmail = chkWeekEmail.Checked;
                user.Active = chkActive.Checked;

                if (ddlAccess.SelectedItem.Text != "MasterAdmin")
                {
                    user.UserType = ddlUserType.SelectedItem.Text;
                }
                else
                {
                    user.UserType = string.Empty;
                }

                CZBizObjects.UserList.AddItem(user);
            }
            else
            {
                user.DisplayName = txtDisplayName.Text;

                if (pwdchanged)
                    user.ExpirationDate = System.DateTime.Now.AddDays(90);

                user.NewEmail = chkNewEmail.Checked;
                user.CompEmail = chkCompEmail.Checked;
                user.ObserveEmail = chkObserveEmail.Checked;
                user.WeekEmail = chkWeekEmail.Checked;
                user.Active = chkActive.Checked;
                if (ddlAccess.SelectedItem.Text != "MasterAdmin")
                {
                    user.UserType = ddlUserType.SelectedItem.Text;
                }
                else
                {
                    user.UserType = string.Empty;
                }

                CZBizObjects.UserList.UpdateItem(user);

                //now save their regions if applicable.                
                if (user.UserType.IsNotNullOrEmpty())
                {                                        
                    UserRegionList.DeleteItems(user.Username);

                    foreach (ListItem li in cbxRegions.Items)
                    {
                        if (li.Selected)
                        {
                            UserRegion region = new UserRegion();
                            region.AnimalRegionCode = li.Value;
                            region.Username = user.Username;
                            region.Active = true;

                            UserRegionList.AddItem(region);                         

                        }
                    }
                    
                }
                else
                {
                    UserRegionList.DeleteItems(user.Username); //clean up
                }
                
            }
            

            return true;

        }

        private string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
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

        protected void btnSaveEmail_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveRegions_Click(object sender, EventArgs e)
        {

        }

    }
}