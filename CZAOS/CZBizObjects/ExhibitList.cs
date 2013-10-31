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
	[DataObject()] public class ExhibitList  : BaseList<Exhibit>
	{
		#region CONSTRUCTION/LOAD

		 public ExhibitList()
        {
        }

        public ExhibitList(List<Exhibit> items)
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
		public static ExhibitList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("Exhibit");
			return new ExhibitList(ExhibitProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ExhibitList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Exhibit GetItem(int id)
		{
			return ExhibitProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ExhibitProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ExhibitProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Exhibit AddItem(Exhibit item)
        {
            Exhibit nw = ExhibitProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ExhibitID);
            RemoveCacheList();

            return nw;              
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Exhibit item)
        {
            Exhibit original = Get(item.ExhibitID);

            AuditUpdate(original, item, item.ExhibitID);

            ExhibitProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Exhibit item)
        {          
            DeleteItem(item.ExhibitID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ExhibitProvider.Instance().DeleteItem(id);
            Audit(typeof(Exhibit), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();   
            
        }

        public static bool ItemExists(string exhibit, string regionCode, int itemId)
        {
            ExhibitList list = GetItemCollection(0, 1, "", "Exhibit = '{0}' AND AnimalRegionCode = '{1}'".FormatWith(exhibit, regionCode));
            return list.Any(x => x.ExhibitID != itemId);
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "Exhibit ASC", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitList GetActive()
        {
            List<Exhibit> items = GetCacheList();

            if (items == null)
            {
                items = ExhibitList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ExhibitList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ExhibitList GetActiveByRegion(string regionCode)
        {
            List<Exhibit> items = GetActive();

            IEnumerable<Exhibit> exhibitsByRegion = items.Where(
                    s => s.AnimalRegionCode == regionCode);

            return new ExhibitList(exhibitsByRegion.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Exhibit Get(int id)
        {
            ExhibitList items = GetActive();
            Exhibit item = items.FirstOrDefault(s => s.ExhibitID == id);

            return item;
        }

		#endregion

	}   

}



