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
	[DataObject()] public class WeatherConditionList  : BaseList<WeatherCondition>
	{
		#region CONSTRUCTION/LOAD

		 public WeatherConditionList()
        {
        }

        public WeatherConditionList(List<WeatherCondition> items)
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
		public static WeatherConditionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Weather");
			return new WeatherConditionList(WeatherConditionProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherConditionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherConditionList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherConditionList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WeatherCondition GetItem(int id)
		{
			return WeatherConditionProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return WeatherConditionProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return WeatherConditionProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static WeatherCondition AddItem(WeatherCondition item)
        {
            WeatherCondition nw = WeatherConditionProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.WeatherID);
            RemoveCacheList();

            return nw;               
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(WeatherCondition item)
        {
            WeatherCondition original = Get(item.WeatherID);

            AuditUpdate(original, item, item.WeatherID);

            WeatherConditionProvider.Instance().UpdateItem(item);

            RemoveCacheList(); 

        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(WeatherCondition item)
        {          
            DeleteItem(item.WeatherID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            WeatherConditionProvider.Instance().DeleteItem(id);
            Audit(typeof(WeatherCondition), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList(); 
            
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherConditionList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "Weather", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherConditionList GetActive()
        {
            List<WeatherCondition> items = GetCacheList();

            if (items == null)
            {
                items = WeatherConditionList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new WeatherConditionList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WeatherCondition Get(int id)
        {
            WeatherConditionList items = GetActive();
            WeatherCondition item = items.FirstOrDefault(s => s.WeatherID == id);

            return item;
        }

        #endregion     
		
	}   
	
	

	
	
}



