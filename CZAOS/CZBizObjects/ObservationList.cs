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
	[DataObject()] public class ObservationList  : BaseList<Observation>
	{
		#region CONSTRUCTION/LOAD

		 public ObservationList()
        {
        }

        public ObservationList(List<Observation> items)
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
		public static ObservationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObserveStart");
			return new ObservationList(ObservationProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObservationList GetItemCollection(bool active)
		{
			string filter = string.Empty;
			
			if(!active)
			{
                filter = "Deleted = 1";
			}
			else
			{
                filter = "Deleted = 0";
			}
				
			return GetItemCollection(0, 0, string.Empty, filter);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Observation GetItem(int id)
		{
			return ObservationProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ObservationProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ObservationProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Observation AddItem(Observation item)
        {
            Observation nw = ObservationProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ObservationID);
            RemoveCacheList();

            return nw;                     
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Observation item)
        {
            Observation original = Get(item.ObservationID);

            AuditUpdate(original, item, item.ObservationID);

            ObservationProvider.Instance().UpdateItem(item);

            RemoveCacheList();               
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Observation item)
        {          
            DeleteItem(item.ObservationID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ObservationProvider.Instance().DeleteItem(id);
            Audit(typeof(Observation), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();    
        }

		#endregion

		#region PUBLIC METHODS       

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObservationList GetActive()
        {
            List<Observation> items = GetCacheList();

            if (items == null)
            {
                items = ObservationList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ObservationList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Observation Get(int id)
        {
            ObservationList items = GetActive();
            Observation item = items.FirstOrDefault(s => s.ObservationID == id);

            return item;
        }

        #endregion     
		
	}   
	
	

	
	
}



