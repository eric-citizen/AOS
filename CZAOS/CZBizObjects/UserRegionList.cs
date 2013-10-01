using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;


namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class UserRegionList  : BaseList<UserRegion>
	{
		#region CONSTRUCTION/LOAD

		 public UserRegionList()
        {
        }

        public UserRegionList(List<UserRegion> items)
        {
            base.AddRange(items);
        }

        public void Load()
        {
            this.AddRange(GetItemCollection());
        }
		#endregion

		#region ADMIN METHODS

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserRegionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("UserName, AnimalRegion");
			return new UserRegionList(UserRegionProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserRegionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserRegionList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserRegionList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserRegion GetItem(int id)
		{
			return UserRegionProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return UserRegionProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return UserRegionProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static UserRegion AddItem(UserRegion item)
        {
            UserRegion nw = UserRegionProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.UserRegionID);
            RemoveCacheList();

            return nw;            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(UserRegion item)
        {
            UserRegion original = Get(item.UserRegionID);

            AuditUpdate(original, item, item.UserRegionID);

            UserRegionProvider.Instance().UpdateItem(item);

            RemoveCacheList();                  
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(UserRegion item)
        {          
            DeleteItem(item.UserRegionID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            UserRegionProvider.Instance().DeleteItem(id);
            Audit(typeof(UserRegion), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();           
            
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItems(string username)
        {
            UserRegionList list = GetActive(username);

            foreach (UserRegion region in list)
            {
                UserRegionProvider.Instance().DeleteItem(region.UserRegionID);
            }
            Audit(typeof(UserRegion), ChangeLog.LogChangeType.delete, username);
            RemoveCacheList();

        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserRegionList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "UserName, AnimalRegion", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserRegionList GetActive()
        {
            List<UserRegion> items = GetCacheList();

            if (items == null)
            {
                items = UserRegionList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new UserRegionList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserRegionList GetActive(string username)
        {
            UserRegionList items = GetActive();

            var users =
                from u in items where u.Username.Equals(username)
                select u;

            return new UserRegionList(users.ToList());

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UserRegion Get(int id)
        {
            UserRegionList items = GetActive();
            UserRegion item = items.FirstOrDefault(s => s.UserRegionID == id);

            return item;
        }
        
        public bool ContainsAnimalRegion(string animalRegionCode)
        {            
            UserRegion item = this.FirstOrDefault(s => s.AnimalRegionCode.Equals(animalRegionCode, StringComparison.InvariantCultureIgnoreCase));
            return item != null;
        }

        #endregion     
		
	}   
	



	
}



