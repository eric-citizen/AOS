using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;
using System.Web;

namespace CZBizObjects
{
	[Serializable()][DataObject()]
	public class WeatherList : BaseList<Weather>	
	{
		#region CONSTRUCTION/LOAD

		 public WeatherList()
        {
        }

        public WeatherList(List<Weather> items)
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
		public static WeatherList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObservationID, WeatherTime");
			return new WeatherList(WeatherProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return new WeatherList(GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return new WeatherList(GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherList GetItemCollection()
		{
			return new WeatherList(GetItemCollection(0, 0, string.Empty, string.Empty));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Weather GetItem(int id)
		{
			return WeatherProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return WeatherProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return WeatherProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Weather AddItem(Weather item)
        {
            Weather nw = WeatherProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ID);

            RemoveCacheList();

            return nw;            
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Weather item)
        {

            Weather original = Get(item.ID);

            AuditUpdate(original, item, item.ID);

            WeatherProvider.Instance().UpdateItem(item);

            RemoveCacheList();
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Weather item)
        {          
            DeleteItem(item.ID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            Audit(typeof(Weather), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();
            WeatherProvider.Instance().DeleteItem(id);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteByObservationID(int observationId)
        {
            Audit(typeof(Weather), ChangeLog.LogChangeType.delete, "observationId = " + observationId);
            RemoveCacheList();
            WeatherProvider.Instance().DeleteByObservationID(observationId);
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherList GetActive()
        {
            List<Weather> items = GetCacheList();

            if (items == null)
            {
                items = WeatherList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new WeatherList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherList GetActive(int observationId)
        {
            List<Weather> items = GetActive();

            IEnumerable<Weather> records = items.Where(o => o.ObservationID == observationId);

            return new WeatherList(records.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Weather Get(int id)
        {
            WeatherList items = GetActive();
            Weather item = items.FirstOrDefault(s => s.ID == id);

            return item;
        }

		#endregion

	}   

}



