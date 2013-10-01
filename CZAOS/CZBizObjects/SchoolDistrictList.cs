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
	[DataObject()] public class SchoolDistrictList  : BaseList<SchoolDistrict>
	{
		#region CONSTRUCTION/LOAD

		 public SchoolDistrictList()
        {
        }

        public SchoolDistrictList(List<SchoolDistrict> items)
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
		public static SchoolDistrictList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("District");
			return new SchoolDistrictList(SchoolDistrictProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolDistrictList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolDistrictList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolDistrictList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}			
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SchoolDistrict GetItem(int id)
		{
			return SchoolDistrictProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return SchoolDistrictProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return SchoolDistrictProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static SchoolDistrict AddItem(SchoolDistrict item)
        {
            SchoolDistrict nw = SchoolDistrictProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.DistrictID);
            RemoveCacheList();

            return nw;
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(SchoolDistrict item)
        {
            SchoolDistrict original = Get(item.DistrictID);

            AuditUpdate(original, item, item.DistrictID);

            SchoolDistrictProvider.Instance().UpdateItem(item);

            RemoveCacheList();            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(SchoolDistrict item)
        {          
            DeleteItem(item.DistrictID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            SchoolDistrictProvider.Instance().DeleteItem(id);
            Audit(typeof(SchoolDistrict), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();               
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SchoolDistrictList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "District ASC", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SchoolDistrictList GetActive()
        {
            List<SchoolDistrict> items = GetCacheList();

            if (items == null)
            {
                items = SchoolDistrictList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            IEnumerable<SchoolDistrict> districtsWithSchools = items.Where(
                   s => s.SchoolCount > 0);

            return new SchoolDistrictList(districtsWithSchools.ToList());
            
        }        

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SchoolDistrict Get(int id)
        {
            SchoolDistrictList items = GetActive();
            SchoolDistrict item = items.FirstOrDefault(s => s.DistrictID == id);

            return item;
        }

		#endregion

	}   

}



