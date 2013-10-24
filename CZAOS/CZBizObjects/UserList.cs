using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using System.Web.Security;
using KT.Extensions;
using CZAOSCore.Enums;

namespace CZBizObjects
{
	[Serializable()]
    [DataObject()]
    public class UserList : BaseList<CZUser>
	{
		#region CONSTRUCTION/LOAD

		 public UserList()
        {
        }

         public UserList(List<CZUser> items)
        {
            base.AddRange(items);
        }

        public void Load()
        {
            this.AddRange(GetItemCollection());
        }

        public void LoadActive()
        {
            this.AddRange(GetItemCollection(0, 0, "", "Active = 1"));
        }

		#endregion

		#region ADMIN METHODS

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Username");
			return new UserList(UserProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (active)
            {
                filter = "Active = 1";
            }
            else
            {
                filter = "Active = 0";
            }

            return GetItemCollection(0, 0, "DisplayName ASC", filter);
        }

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CZUser GetItem(Guid id)
		{
			return UserProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return UserProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return UserProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static CZUser AddItem(CZUser item)
        {
            CZUser nw = UserProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.Username);
            RemoveCacheList();

            return nw;       
            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(CZUser item)
        {
            CZUser original = GetUser(item.Username);
            AuditUpdate(original, item, item.Username);

            UserProvider.Instance().UpdateItem(item);
            RemoveCacheList();
            
        }

        //[DataObjectMethod(DataObjectMethodType.Delete, false)]
        //public static void DeleteItem(CZUser item)
        //{          
        //    DeleteItem(item.UserId);
        //}

        //[DataObjectMethod(DataObjectMethodType.Delete, false)]
        //public static void DeleteItem(Guid id)
        //{          
        //    UserProvider.Instance().DeleteItem(id);
        //}

		#endregion

        #region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserList GetActiveUsers()
        {
            List<CZDataObjects.CZUser> users = GetCacheList();

            if (users == null)
            {
                users = UserList.GetItemCollection(true).ToList();
                AddToCache(users);
            }

            return new UserList(users);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserList GetActiveProfessionalUsers()
        {
            List<CZDataObjects.CZUser> users = GetActiveUsers();

            IEnumerable<CZUser> pros = users.Where(
                  s => s.UserType.Equals(CZAOSCore.Enums.CoreUserTypes.Professional.ToString()));            

            return new UserList(pros.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserList GetActiveAmateurUsers()
        {
            List<CZDataObjects.CZUser> users = GetActiveUsers();

            IEnumerable<CZUser> pros = users.Where(s => s.UserType == CoreUserTypes.Amateur.ToString() || s.UserType == CoreUserTypes.Education.ToString());

            return new UserList(pros.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CZUser GetUser(string username)
        {
            UserList users = GetActiveUsers();
            CZUser user = users.FirstOrDefault(u => u.Username.Equals(username));

            return user;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CZUser GetUserByEmail(string email)
        {
            string username = Membership.GetUserNameByEmail(email);

            if (username.IsNotNullOrEmpty())
            {
                MembershipUser user = Membership.GetUser(username);

                if (user != null)
                {
                    return GetUser(username);
                }
            }

            return null;
            
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CZUser GetUserByID(string id)
        {
            UserList users = GetActiveUsers();
            CZUser user = users.FirstOrDefault(u => u.UserId.Equals(new Guid(id)));

            return user;
        }

        public bool ContainsUser(string username)
        {            
            CZUser user = this.FirstOrDefault(u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            return user != null;
        }

        #endregion     

	}   

}



