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
	[DataObject()] public class ObserverList  : BaseList<Observer>
	{
		#region CONSTRUCTION/LOAD

		 public ObserverList()
        {
        }

        public ObserverList(List<Observer> items)
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
		public static ObserverList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObservationID, Username");
			return new ObserverList(ObserverProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObserverList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObserverList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ObserverList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}	
		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Observer GetItem(int id)
		{
			return ObserverProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return ObserverProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return ObserverProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Observer AddItem(Observer item)
        {
            Observer nw = ObserverProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.ObserverID);
            RemoveCacheList();

            return nw;               
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Observer item)
        {
            Observer original = Get(item.ObserverID);

            AuditUpdate(original, item, item.ObserverID);

            ObserverProvider.Instance().UpdateItem(item);

            RemoveCacheList();                
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Observer item)
        {          
            DeleteItem(item.ObserverID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            ObserverProvider.Instance().DeleteItem(id);
            Audit(typeof(Observer), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();    
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteByObservation(int observationId)
        {
            ObserverProvider.Instance().DeleteByObservation(observationId);            
            RemoveCacheList();
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObserverList GetItemCollection(bool active)
        {
            string filter = string.Empty;

            if (active)
            {
                filter = "Deleted = 0";
            }
            else
            {
                filter = "Deleted = 1";
            }

            return GetItemCollection(0, 0, "ObservationID, Username", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObserverList GetActive()
        {
            List<Observer> items = GetCacheList();

            if (items == null)
            {
                items = ObserverList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new ObserverList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ObserverList GetActive(int observationId)
        {
            List<Observer> items = GetActive();

            IEnumerable<Observer> observers = items.Where(
                    s => s.ObservationID == observationId);

            return new ObserverList(observers.ToList());

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Observer Get(int id)
        {
            ObserverList items = GetActive();
            Observer item = items.FirstOrDefault(s => s.ObserverID == id);

            return item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Observer Get(string username)
        {
            ObserverList items = GetActive();
            Observer item = items.FirstOrDefault(s => s.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));

            return item;
        }

        public bool ContainsUser(string username)
        {
            IEnumerable<Observer> observers = this.Where(s => s.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));

            return observers.Count() > 0;
        }

        #endregion     
		
	}   
	


	
}



