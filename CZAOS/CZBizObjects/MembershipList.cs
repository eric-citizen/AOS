using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web.Security;

namespace CZBizObjects
{
    [Serializable()]
    [DataObject()] 
    public class MembershipList : BaseList<MembershipUser>
    {

        #region " CONSTRUCTORS "


        public MembershipList()
        {
        }

        public MembershipList(List<MembershipUser> items)
            : base(items)
        {
        }

        //public MembershipList(bool populate)
        //    : base(GetUserCollection)
        //{
        //}

        public MembershipList(int startRowIndex, int maximumRows)
            : this(GetUserCollection(startRowIndex, maximumRows))
        {
        }

        #endregion

        private static int mintTotalUsers;        

        public static MembershipList GetUserCollection()
        {

            MembershipUserCollection members = Membership.GetAllUsers();
            MembershipList list = new MembershipList();
            UserList activeAdmins = new UserList();
            List<string> userIds = new List<string>();

            activeAdmins.Load();

            foreach (CZDataObjects.CZUser admin in activeAdmins)
            {
                userIds.Add(admin.UserId.ToString());
            }

            foreach (MembershipUser user in members)
            {
                //if (userIds.Contains(user.ProviderUserKey.ToString()))
                //{
                 list.Add(user);
                //}
            }

            return list;

        }

        public static MembershipList GetUserCollection(int pageIndex, int pageSize)
        {

            MembershipUserCollection members = Membership.GetAllUsers(pageIndex, pageSize, out mintTotalUsers);
            MembershipList list = new MembershipList();
            UserList activeAdmins = new UserList();
            List<string> userIds = new List<string>();

            activeAdmins.Load();

            foreach (CZDataObjects.CZUser admin in activeAdmins)
            {
                userIds.Add(admin.UserId.ToString());
            }

            foreach (MembershipUser user in members)
            {
                //if (userIds.Contains(user.ProviderUserKey.ToString()))
                //{
                   list.Add(user);
                //}
            }

            return list;

        }

        // pageIndex, pageSize, startsWith, sortExpression, maximumRows.]
        public static MembershipList GetUserCollection(int pageIndex, int pageSize, int maximumRows, string sortExpression, string role, string startsWith)
        {

            return GetUserCollectionByRole(pageIndex, pageSize, maximumRows, sortExpression, role, startsWith);

        }

        public static MembershipList GetUserCollectionByRole(int pageIndex, int pageSize, int maximumRows, string sortExpression, string role, string startsWith)
        {

            MembershipList list = new MembershipList();
            UserList activeAdmins = new UserList();
            List<string> userIds = new List<string>();

            activeAdmins.LoadActive();

            foreach (CZDataObjects.CZUser admin in activeAdmins)
            {
                userIds.Add(admin.UserId.ToString());
            }


            if (role == "Undefined")
            {
                MembershipUserCollection members = Membership.GetAllUsers();

                foreach (MembershipUser user in members)
                {
                    if (userIds.Contains(user.ProviderUserKey.ToString()))
                    {
                        if (string.IsNullOrEmpty(startsWith))
                        {
                            list.Add(user);

                        }
                        else
                        {
                            //if (user.UserName.ToLower().StartsWith(startsWith.ToLower()))
                            //{
                                list.Add(user);
                            //}

                        }

                    }

                }


            }
            else
            {
                string[] users = Roles.GetUsersInRole(role);


                foreach (string user in users)
                {
                    MembershipUser admin = default(MembershipUser);


                    if (string.IsNullOrEmpty(startsWith))
                    {
                        admin = Membership.GetUser(user);

                        if (admin.IsApproved == true)
                        {
                            list.Add(admin);
                        }

                    }
                    else
                    {
                        if (user.ToLower().StartsWith(startsWith.ToLower()))
                        {
                            admin = Membership.GetUser(user);

                            if (admin.IsApproved == true)
                            {
                                list.Add(admin);
                            }

                        }

                    }

                }

            }

            list.Sort(new SortManager(sortExpression));  
             
            if (list.Count > maximumRows && maximumRows > 0 && pageSize > 0)
            {
                list.RemoveRange(maximumRows, list.Count - maximumRows);
            }

            mintTotalUsers = list.Count;

            return list;

        }


        //public static MembershipList GetInactiveUserCollection()
        //{

        //    MembershipList list = new MembershipList();
        //    SiteAdminList inactiveAdmins = SiteAdminList.GetInactive;
        //    List<string> userIds = new List<string>();

        //    foreach (DataObjects.SiteAdmin admin in inactiveAdmins)
        //    {
        //        userIds.Add(admin.UserId);
        //    }

        //    MembershipUserCollection members = Membership.GetAllUsers();


        //    foreach (MembershipUser user in members)
        //    {

        //        if (userIds.Contains(user.ProviderUserKey.ToString))
        //        {
        //            list.Add(user);

        //        }

        //    }

        //    mintTotalUsers = list.Count;

        //    return list;

        //}

        public static int GetCount(int pageIndex, int pageSize, string role, string startsWith)
        {

            return mintTotalUsers;

        }

        public static MembershipUser GetCurrentUser()
        {

            return Membership.GetUser();

        }

        public static MembershipUser GetUser(object userGuid)
        {

            return Membership.GetUser(userGuid);

        }

        public static MembershipUser GetUser(string userName)
        {

            return Membership.GetUser(userName);

        }

        public static string GetUserNameByEmail(string email)
        {

            string s = Membership.GetUserNameByEmail(email);
            return s;

        }

    }

}
