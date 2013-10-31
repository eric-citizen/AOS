using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CZDataObjects;
using KT.Extensions;
using System.Web;
using System.Web.Security;

namespace CZBizObjects
{
	[Serializable()]
	[DataObject()] public class WindList  : BaseList<Wind>
	{
		#region CONSTRUCTION/LOAD

		 public WindList()
        {
        }

        public WindList(List<Wind> items)
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
		public static WindList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Wind");
			return new WindList(WindProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WindList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WindList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static WindList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Wind GetItem(int id)
		{
			return WindProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return WindProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return WindProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Wind AddItem(Wind item)
        {          
            Wind nw =  WindProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.WindID);

            RemoveCacheList();

            return nw;
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Wind item)
        {
            Wind original = Get(item.WindID);

            AuditUpdate(original, item, item.WindID);      

            WindProvider.Instance().UpdateItem(item);

            RemoveCacheList();
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Wind item)
        {            
            DeleteItem(item.WindID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            WindProvider.Instance().DeleteItem(id);
            Audit(typeof(Wind), ChangeLog.LogChangeType.delete, id);            
            RemoveCacheList();
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WindList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "WindID", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static WindList GetActive()
        {
            List<Wind> items = GetCacheList();

            if (items == null)
            {
                items = WindList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new WindList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Wind Get(int id)
        {
            WindList items = GetActive();
            Wind item = items.FirstOrDefault(s => s.WindID == id);

            return item;
        }

        #endregion     
		
	}   

}



