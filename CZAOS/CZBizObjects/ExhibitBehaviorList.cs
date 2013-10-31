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
	[DataObject()] public class ExhibitBehaviorList  : BaseList<ExhibitBehavior>
	{
		#region CONSTRUCTION/LOAD

		 public ExhibitBehaviorList()
        {
        }

        public ExhibitBehaviorList(List<ExhibitBehavior> items)
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
		public static ExhibitBehaviorList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Behavior");
			return new ExhibitBehaviorList(ExhibitBehaviorProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitBehaviorList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitBehaviorList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitBehaviorList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitBehavior GetItem(int id)
		{
			return ExhibitBehaviorProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ExhibitBehaviorProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ExhibitBehaviorProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static ExhibitBehavior AddItem(ExhibitBehavior item)
        {
            ExhibitBehavior nw = ExhibitBehaviorProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ExhibitBehaviorID);
            RemoveCacheList();

            return nw;              

        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(ExhibitBehavior item)
        {
            ExhibitBehavior original = Get(item.ExhibitBehaviorID);

            AuditUpdate(original, item, item.ExhibitBehaviorID);

            ExhibitBehaviorProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(ExhibitBehavior item)
        {          
            DeleteItem(item.ExhibitBehaviorID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ExhibitBehaviorProvider.Instance().DeleteItem(id);
            Audit(typeof(ExhibitBehavior), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();         
            
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitBehaviorList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "Behavior", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitBehaviorList GetActive()
        {
            List<ExhibitBehavior> items = GetCacheList();

            if (items == null)
            {
                items = ExhibitBehaviorList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ExhibitBehaviorList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitBehavior Get(int id)
        {
            ExhibitBehaviorList items = GetActive();
            ExhibitBehavior item = items.FirstOrDefault(s => s.ExhibitBehaviorID == id);

            return item;
        }

		#endregion

	}   

}



