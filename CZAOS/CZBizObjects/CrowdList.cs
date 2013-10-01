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
	[DataObject()] public class CrowdList  : BaseList<Crowd>
	{
		#region CONSTRUCTION/LOAD

		 public CrowdList()
        {
        }

        public CrowdList(List<Crowd> items)
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
		public static CrowdList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Crowd");
			return new CrowdList(CrowdProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CrowdList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CrowdList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CrowdList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CrowdList GetItemCollection(bool active)
		{
			string filter = string.Empty;
			
			if(active)
				filter = "Active = 1";
			else
				filter = "Active = 0";
				
			return GetItemCollection(0, 0, string.Empty, filter);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Crowd GetItem(int id)
		{
			return CrowdProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return CrowdProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return CrowdProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Crowd AddItem(Crowd item)
        {
            Crowd nw = CrowdProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.CrowdID);
            RemoveCacheList();

            return nw;              
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Crowd item)
        {
            Crowd original = Get(item.CrowdID);

            AuditUpdate(original, item, item.CrowdID);

            CrowdProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Crowd item)
        {          
            DeleteItem(item.CrowdID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            CrowdProvider.Instance().DeleteItem(id);
            Audit(typeof(Crowd), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                
        }

		#endregion

		#region PUBLIC METHODS        

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CrowdList GetActive()
        {
            List<Crowd> items = GetCacheList();

            if (items == null)
            {
                items = CrowdList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new CrowdList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Crowd Get(int id)
        {
            CrowdList items = GetActive();
            Crowd item = items.FirstOrDefault(s => s.CrowdID == id);

            return item;
        }

		#endregion

	}   

}



