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
	[DataObject()] public class ObservationRecordList  : BaseList<ObservationRecord>
	{
		#region CONSTRUCTION/LOAD

		 public ObservationRecordList()
        {
        }

        public ObservationRecordList(List<ObservationRecord> items)
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
		public static ObservationRecordList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObservationID, Username, ObserverTime");
			return new ObservationRecordList(ObservationRecordProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationRecordList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationRecordList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationRecordList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}        

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationRecord GetItem(int id)
		{
			return ObservationRecordProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ObservationRecordProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ObservationRecordProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static ObservationRecord AddItem(ObservationRecord item)
        {
            ObservationRecord nw = ObservationRecordProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ObservationRecordID);
            RemoveCacheList();

            return nw;                      
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(ObservationRecord item)
        {
            ObservationRecord original = Get(item.ObservationRecordID);

            AuditUpdate(original, item, item.ObservationRecordID);

            ObservationRecordProvider.Instance().UpdateItem(item);

            RemoveCacheList();            
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(ObservationRecord item)
        {          
            DeleteItem(item.ObservationRecordID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ObservationRecordProvider.Instance().DeleteItem(id);
            Audit(typeof(ObservationRecord), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                 
        }

        public static void DeleteByObservation(int observationId)
        {
            ObservationRecordProvider.Instance().DeleteByObservation(observationId);
            Audit(typeof(ObservationRecord), ChangeLog.LogChangeType.delete, 0);
            RemoveCacheList(); 
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationRecordList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (!active)
            {
                filter = "Deleted = 1";
            }
            else
            {
                filter = "Deleted = 0";
            }

            return GetItemCollection(0, 0, "ObservationID, Username, ObserverTime", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationRecordList GetActive()
        {
            List<ObservationRecord> items = GetCacheList();

            if (items == null)
            {
                items = ObservationRecordList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ObservationRecordList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationRecordList GetActive(int observationId)
        {
            List<ObservationRecord> items = GetActive();

            IEnumerable<ObservationRecord> records = items.Where(o => o.ObservationID == observationId);

            return new ObservationRecordList(records.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationRecord Get(int id)
        {
            ObservationRecordList items = GetActive();
            ObservationRecord item = items.FirstOrDefault(s => s.ObservationRecordID == id);

            return item;
        }

		#endregion

	}   

}



