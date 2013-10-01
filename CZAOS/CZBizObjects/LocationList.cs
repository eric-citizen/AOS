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
	[DataObject()] public class LocationList  : BaseList<Location>
	{
		#region CONSTRUCTION/LOAD

		 public LocationList()
        {
        }

        public LocationList(List<Location> items)
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
		public static LocationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("[SortOrder]");
			return new LocationList(LocationProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LocationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LocationList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LocationList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}        

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Location GetItem(int id)
		{
			return LocationProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return LocationProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return LocationProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Location AddItem(Location item)
        {
            Location nw = LocationProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.LocationID);
            RemoveCacheList();

            return nw;                      
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Location item)
        {
            Location original = Get(item.LocationID);

            AuditUpdate(original, item, item.LocationID);

            LocationProvider.Instance().UpdateItem(item);

            RemoveCacheList();            
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Location item)
        {          
            DeleteItem(item.LocationID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            LocationProvider.Instance().DeleteItem(id);
            Audit(typeof(Location), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                 
        }

        public static void UpdateSort(List<ISortable> items)
        {
            LocationProvider.Instance().UpdateSort(items);
            Audit(typeof(Location), ChangeLog.LogChangeType.sort, 0);
            RemoveCacheList(); 
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LocationList GetItemCollection(bool active)
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
        public static LocationList GetActive()
        {
            List<Location> items = GetCacheList();

            if (items == null)
            {
                items = LocationList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new LocationList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Location Get(int id)
        {
            LocationList items = GetActive();
            Location item = items.FirstOrDefault(s => s.LocationID == id);

            return item;
        }

		#endregion

	}   

}



