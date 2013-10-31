using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using CZDataObjects.Interfaces;
using KT.Extensions;

namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class BehaviorList  : BaseList<Behavior>
	{
		#region CONSTRUCTION/LOAD

		 public BehaviorList()
        {
        }

        public BehaviorList(List<Behavior> items)
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
		public static BehaviorList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Behavior"); 
			return new BehaviorList(BehaviorProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BehaviorList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorList GetActiveItemCollection()
        {
            return GetItemCollection(0, 0, string.Empty, "Active = 1");
        }

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Behavior GetItem(int id)
		{
			return BehaviorProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return BehaviorProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return BehaviorProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Behavior AddItem(Behavior item)
        {
            Behavior nw = BehaviorProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.BehaviorID);
            RemoveCacheList();

            return nw;  
            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Behavior item)
        {
            Behavior original = Get(item.BehaviorID);

            AuditUpdate(original, item, item.BehaviorID);

            BehaviorProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Behavior item)
        {          
            DeleteItem(item.BehaviorID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            BehaviorProvider.Instance().DeleteItem(id);
            Audit(typeof(Behavior), ChangeLog.LogChangeType.delete, id);            
            RemoveCacheList();    
            
        }

        public static void UpdateSort(List<ISortable> items)
        {
            BehaviorProvider.Instance().UpdateSort(items);
            Audit(typeof(Behavior), ChangeLog.LogChangeType.sort, 0);
            RemoveCacheList();    
        }

        public static bool ItemExists(string behavior, string code, int categoryId, int itemId)
        {
            BehaviorList list = GetItemCollection(0, 1, "", "Behavior = '{0}' AND BehaviorCode = '{1}'".FormatWith(behavior, code));
            return list.Any(x => (x.BvrCatID == categoryId && x.BehaviorID != itemId));
        }


		#endregion

        #region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorList GetItemCollection(bool active)
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

            return new BehaviorList(GetItemCollection(0, 0, "SortOrder ASC, Behavior ASC", filter));
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static BehaviorList GetActive()
        {
            List<CZDataObjects.Behavior> items = GetCacheList();

            if (items == null)
            {
                items = BehaviorList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new BehaviorList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Behavior Get(int id)
        {
            BehaviorList items = GetActive();
            Behavior item = items.FirstOrDefault(s => s.BehaviorID == id);

            return item;
        }

        #endregion     

	}   

}



