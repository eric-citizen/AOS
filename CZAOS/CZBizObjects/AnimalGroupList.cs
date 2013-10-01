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
	[DataObject()] public class AnimalGroupList  : BaseList<AnimalGroup>
	{
		#region CONSTRUCTION/LOAD

		 public AnimalGroupList()
        {
        }

        public AnimalGroupList(List<AnimalGroup> items)
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
		public static AnimalGroupList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{			
			sortExpression = sortExpression.EnsureNotNull("ObservationID, GrpId");
			return new AnimalGroupList(AnimalGroupProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalGroupList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalGroupList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalGroupList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalGroup GetItem(int id)
		{
			return AnimalGroupProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return AnimalGroupProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return AnimalGroupProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static AnimalGroup AddItem(AnimalGroup item)
        {         
			AnimalGroup nw = AnimalGroupProvider.Instance().AddItem(item);

            Audit(nw, ChangeLog.LogChangeType.create, nw.AnimalGroupID);

            RemoveCacheList();

            return nw;                   
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(AnimalGroup item)
        {      
			AnimalGroup original = Get(item.AnimalGroupID);

            AuditUpdate(original, item, item.AnimalGroupID);

            AnimalGroupProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }		

        public static void DeleteByObservation(int observationId)
        {
            AnimalGroupProvider.Instance().DeleteItemByObservationID(observationId);
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalGroupList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "ObservationID, GrpId", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalGroupList GetActive()
        {
            List<AnimalGroup> items = GetCacheList();

            if (items == null)
            {
                items = AnimalGroupList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new AnimalGroupList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalGroupList GetActiveByObservationID(int observationId)
        {
            List<AnimalGroup> items = GetActive();

            IEnumerable<AnimalGroup> groups = items.Where(s => s.ObservationID == observationId);

            return new AnimalGroupList(groups.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalGroup Get(int id)
        {
            AnimalGroupList items = GetActive();
            AnimalGroup item = items.FirstOrDefault(s => s.AnimalGroupID == id);

            return item;
        }        

        #endregion     
		
	}   
	
	

	
}



