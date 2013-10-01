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
	[DataObject()] public class GradeList  : BaseList<Grade>
	{
		#region CONSTRUCTION/LOAD

		 public GradeList()
        {
        }

        public GradeList(List<Grade> items)
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
		public static GradeList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("[GradeId]");
			return new GradeList(GradeProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GradeList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GradeList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GradeList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Grade GetItem(int id)
		{
			return GradeProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return GradeProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return GradeProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Grade AddItem(Grade item)
        {
            Grade nw = GradeProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.GradeID);
            RemoveCacheList();

            return nw;                
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Grade item)
        {
            Grade original = Get(item.GradeID);

            AuditUpdate(original, item, item.GradeID);

            GradeProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Grade item)
        {          
            DeleteItem(item.GradeID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            GradeProvider.Instance().DeleteItem(id);
            Audit(typeof(Grade), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static GradeList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "GradeId", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static GradeList GetActive()
        {
            List<Grade> items = GetCacheList();

            if (items == null)
            {
                items = GradeList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new GradeList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Grade Get(int id)
        {
            GradeList items = GetActive();
            Grade item = items.FirstOrDefault(s => s.GradeID == id);

            return item;
        }

		#endregion

	}   

}



