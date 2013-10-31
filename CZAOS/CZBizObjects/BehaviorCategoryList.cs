using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;
using CZDataObjects.Interfaces;

namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class BehaviorCategoryList  : BaseList<BehaviorCategory>
	{
		#region CONSTRUCTION/LOAD

		 public BehaviorCategoryList()
        {
        }

        public BehaviorCategoryList(List<BehaviorCategory> items)
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
		public static BehaviorCategoryList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("BvrCat"); 
			return new BehaviorCategoryList(BehaviorCategoryProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorCategoryList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorCategoryList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorCategoryList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorCategory GetItem(int id)
		{
			return BehaviorCategoryProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return BehaviorCategoryProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return BehaviorCategoryProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static BehaviorCategory AddItem(BehaviorCategory item)
        {
            BehaviorCategory nw = BehaviorCategoryProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.BvrCatID);
            RemoveCacheList();

            return nw;  
            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(BehaviorCategory item)
        {
            BehaviorCategory original = Get(item.BvrCatID);

            AuditUpdate(original, item, item.BvrCatID);

            BehaviorCategoryProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }
        
        public static void UpdateSort(List<ISortable> items)
        {
            BehaviorCategoryProvider.Instance().UpdateSort(items);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(BehaviorCategory item)
        {          
            DeleteItem(item.BvrCatID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            Audit(typeof(BehaviorCategory), ChangeLog.LogChangeType.delete, id);
            BehaviorCategoryProvider.Instance().DeleteItem(id);
            RemoveCacheList();                   
        }

        public static bool ItemExists(string name, string code, int itemId)
        {
            BehaviorCategoryList list = GetItemCollection(0, 1, "", "BvrCat = '{0}' AND BvrCatCode = '{1}'".FormatWith(name, code));
            return list.Any(x => x.BvrCatID != itemId);

        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorCategoryList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "SortOrder ASC", filter);
        }

	    [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorCategoryList GetActive()
        {
            List<BehaviorCategory> items = GetCacheList();

            if (items == null)
            {
                items = BehaviorCategoryList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new BehaviorCategoryList(items);
        }

	    [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorCategory Get(int id)
        {
            BehaviorCategoryList items = GetActive();
            BehaviorCategory item = items.FirstOrDefault(s => s.BvrCatID == id);

            return item;
        }

	    #endregion
	}   

}



