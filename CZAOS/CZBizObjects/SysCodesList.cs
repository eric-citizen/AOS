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
	[DataObject()] public class SysCodeList  : BaseList<SysCode>
	{
		#region CONSTRUCTION/LOAD

		 public SysCodeList()
        {
        }

        public SysCodeList(List<SysCode> items)
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
		public static SysCodeList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("");
			return new SysCodeList(SysCodeProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SysCodeList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SysCodeList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SysCodeList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SysCode GetItem(int id)
		{
			return SysCodeProvider.Instance().GetItemCollection(0,0,"","ID = " + id)[0];
		}

		public static int GetCount()
        {
            return SysCodeProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return SysCodeProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static SysCode AddItem(SysCode item)
        {         
			SysCode nw = SysCodeProvider.Instance().AddItem(item);

            Audit(nw, ChangeLog.LogChangeType.create, nw.Id);

            RemoveCacheList();

            return nw;                   
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(SysCode item)
        {
            SysCode original = Get(item.Id);

            AuditUpdate(original, item, item.Id);

            SysCodeProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(SysCode item)
        {
            DeleteItem(item.Id);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {    
			Audit(typeof(SysCode), ChangeLog.LogChangeType.delete, id);
            SysCodeProvider.Instance().DeleteItem(id);
            RemoveCacheList();           		
            
        }

		#endregion

		#region PUBLIC METHODS        

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SysCodeList GetAll()
        {
            List<SysCode> items = GetCacheList();

            if (items == null)
            {
                items = SysCodeList.GetItemCollection().ToList();
                AddToCache(items);
            }

            return new SysCodeList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SysCode Get(int id)
        {
            SysCodeList items = GetAll();
            SysCode item = items.FirstOrDefault(s => s.Id == id);

            return item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SysCode Get(string key)
        {
            SysCodeList items = GetAll();
            SysCode item = items.FirstOrDefault(s => s.Description.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            return item;
        }

        public static bool KeyExists(int id, string key)
        {
            SysCode item = Get(key);

            if (id == 0)
                return item != null;            

            if (item == null)
                return false;

            return !item.Id.Equals(id) && item.Description.Equals(key);
        }

        #endregion     		
	}   
	
	


	
}



