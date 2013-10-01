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
	[DataObject()] public class ExhibitLocationList  : BaseList<ExhibitLocation>
	{
		#region CONSTRUCTION/LOAD

		 public ExhibitLocationList()
        {
        }

        public ExhibitLocationList(List<ExhibitLocation> items)
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
		public static ExhibitLocationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Exhibit, Location");
			return new ExhibitLocationList(ExhibitLocationProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitLocationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitLocationList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitLocationList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitLocation GetItem(int id)
		{
			return ExhibitLocationProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ExhibitLocationProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ExhibitLocationProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static ExhibitLocation AddItem(ExhibitLocation item)
        {
            ExhibitLocation nw = ExhibitLocationProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ExhibitLocationID);
            RemoveCacheList();

            return nw;      

        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(ExhibitLocation item)
        {
            ExhibitLocation original = Get(item.ExhibitLocationID);

            AuditUpdate(original, item, item.ExhibitLocationID);

            ExhibitLocationProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(ExhibitLocation item)
        {          
            DeleteItem(item.ExhibitLocationID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ExhibitLocationProvider.Instance().DeleteItem(id);
            Audit(typeof(ExhibitLocation), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList(); 
            
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitLocationList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "Exhibit ASC, Location ASC", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitLocationList GetActive()
        {
            List<ExhibitLocation> items = GetCacheList();

            if (items == null)
            {
                items = ExhibitLocationList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ExhibitLocationList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitLocation Get(int id)
        {
            ExhibitLocationList items = GetActive();
            ExhibitLocation item = items.FirstOrDefault(s => s.ExhibitLocationID == id);

            return item;
        }

		#endregion

	}   

}



