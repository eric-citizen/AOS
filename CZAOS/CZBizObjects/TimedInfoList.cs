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
	[DataObject()] public class TimedInfoList  : BaseList<TimedInfo>
	{
		#region CONSTRUCTION/LOAD

		 public TimedInfoList()
        {
        }

        public TimedInfoList(List<TimedInfo> items)
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
		public static TimedInfoList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("TimeStart, TimeEnd");
			return new TimedInfoList(TimedInfoProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TimedInfoList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TimedInfoList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TimedInfoList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TimedInfo GetItem(int id)
		{
			return TimedInfoProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return TimedInfoProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return TimedInfoProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static TimedInfo AddItem(TimedInfo item)
        {
            TimedInfo nw = TimedInfoProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.TimedInfoID);
            RemoveCacheList();

            return nw;                
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(TimedInfo item)
        {
            TimedInfo original = Get(item.TimedInfoID);

            AuditUpdate(original, item, item.TimedInfoID);

            TimedInfoProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(TimedInfo item)
        {          
            DeleteItem(item.TimedInfoID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            TimedInfoProvider.Instance().DeleteItem(id);
            Audit(typeof(TimedInfo), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TimedInfoList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "TimeStart, TimeEnd", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TimedInfoList GetActive()
        {
            List<TimedInfo> items = GetCacheList();

            if (items == null)
            {
                items = TimedInfoList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new TimedInfoList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TimedInfo Get(int id)
        {
            TimedInfoList items = GetActive();
            TimedInfo item = items.FirstOrDefault(s => s.TimedInfoID == id);

            return item;
        }

		#endregion

	}   

}



