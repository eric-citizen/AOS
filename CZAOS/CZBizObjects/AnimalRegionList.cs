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
    [DataObject()]
    public class AnimalRegionList : BaseList<AnimalRegion>
	{
		#region CONSTRUCTION/LOAD

		 public AnimalRegionList()
        {
        }

         public AnimalRegionList(List<AnimalRegion> items)
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
		public static AnimalRegionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("AnimalRegion, RegionName"); 
			return new AnimalRegionList(AnimalRegionProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalRegionList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalRegionList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalRegionList GetActiveItemCollection()
		{
            return GetItemCollection(0, 0, "AnimalRegion ASC", "Active  = 1");
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalRegionList GetItemCollection()
        {
            return GetItemCollection(0, 0, string.Empty, string.Empty);
        }

		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalRegion GetItem(string animalRegionCode)
		{
            return AnimalRegionProvider.Instance().GetItem(animalRegionCode);
		}

		public static int GetCount()
        {
            return AnimalRegionProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return AnimalRegionProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static AnimalRegion AddItem(AnimalRegion item)
        {
            AnimalRegion nw = AnimalRegionProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.AnimalRegionCode);
            RemoveCacheList();

            return nw;  
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(AnimalRegion item)
        {
            AnimalRegion original = Get(item.AnimalRegionCode);

            AuditUpdate(original, item, item.AnimalRegionCode);

            AnimalRegionProvider.Instance().UpdateItem(item);

            RemoveCacheList();
        }

        //[DataObjectMethod(DataObjectMethodType.Delete, false)]
        //public static void DeleteItem(AnimalRegion item)
        //{
        //    item.Active = false;
        //    UpdateItem(item);
        //}

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(string animalRegionCode)
        {
            Audit(typeof(AnimalRegion), ChangeLog.LogChangeType.delete, animalRegionCode);
            AnimalRegionProvider.Instance().DeleteItem(animalRegionCode);
            RemoveCacheList();             
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool RegionCodeExists(string animalRegionCode)
        {
            AnimalRegion item = GetItem(animalRegionCode);

            return item != null;
        }

		#endregion

        #region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalRegionList GetItemCollection(bool active)
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

            return new AnimalRegionList(GetItemCollection(0, 0, "RegionName ASC, AnimalRegion ASC", filter));
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalRegionList GetActive()
        {
            List<CZDataObjects.AnimalRegion> items = GetCacheList();

            if (items == null)
            {
                items = AnimalRegionList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            IEnumerable<AnimalRegion> regionsWithExhibits = items.Where(
                   s => s.ExhibitCount > 0);

            return new AnimalRegionList(regionsWithExhibits.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalRegion Get(string animalRegionCode)
        {
            AnimalRegionList items = GetActive();
            AnimalRegion item = items.FirstOrDefault(s => s.AnimalRegionCode == animalRegionCode);

            return item;
        }

        #endregion     

	}   

}



