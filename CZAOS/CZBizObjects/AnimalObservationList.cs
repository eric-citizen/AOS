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
	[DataObject()] public class AnimalObservationList  : BaseList<AnimalObservation>
	{
		#region CONSTRUCTION/LOAD

		 public AnimalObservationList()
        {
        }

        public AnimalObservationList(List<AnimalObservation> items)
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
		public static AnimalObservationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("AnimalObservation");
			return new AnimalObservationList(AnimalObservationProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalObservationList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalObservationList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalObservationList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnimalObservationList GetItemCollection(bool active)
		{
			string filter = string.Empty;
			
			if(!active)
				filter = "Deleted = 1";
			else
                filter = "Deleted = 0";
				
			return GetItemCollection(0, 0, "GrpId, AnimalId", filter);
		}	

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static AnimalObservation AddItem(AnimalObservation item)
        {
            AnimalObservation nw = AnimalObservationProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.AnimalObservationID);
            RemoveCacheList();

            return nw;              
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(AnimalObservation item)
        {            
            AnimalObservation original = GetItemCollection(0,1,"", "ID = " + item.AnimalObservationID)[0];

            AuditUpdate(original, item, item.AnimalObservationID);

            AnimalObservationProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteByObservation(AnimalObservation item)
        {
            DeleteByObservation(item.ObservationID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteByObservation(int observationId)
        {
            AnimalObservationProvider.Instance().DeleteByObservation(observationId);
            Audit(typeof(AnimalObservation), ChangeLog.LogChangeType.delete, observationId);
            RemoveCacheList();                
        }

		#endregion

		#region PUBLIC METHODS        

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalObservationList GetActive()
        {
            List<AnimalObservation> items = GetCacheList();

            if (items == null)
            {
                items = AnimalObservationList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new AnimalObservationList(items);
            
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalObservationList GetActive(int observationId)
        {
            List<AnimalObservation> items = GetActive();            

            IEnumerable<AnimalObservation> animals = items.Where(
                  s => s.ObservationID.Equals(observationId));

            return new AnimalObservationList(animals.ToList());

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AnimalObservationList GetActive(int observationId, int groupId)
        {
            List<AnimalObservation> items = GetActive();

            IEnumerable<AnimalObservation> animals = items.Where(
                  s => s.ObservationID.Equals(observationId) && s.GroupID.Equals(groupId));

            return new AnimalObservationList(animals.ToList());

        }


        public AnimalObservationList GetByGroup(int groupId)
        {            

            IEnumerable<AnimalObservation> animals = this.Where(
                  s => s.GroupID.Equals(groupId));

            return new AnimalObservationList(animals.ToList());

        }

        public List<int> GetAnimalIDsByGroup(int groupId)
        {
            AnimalObservationList aol = this.GetByGroup(groupId);
            List<int> ids = new List<int>();

            foreach (AnimalObservation ao in aol)
            {
                ids.Add(ao.AnimalID);
            }

            return ids;

        }  

		#endregion

	}   

}



