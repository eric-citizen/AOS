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
	[DataObject()] public class NoteList  : BaseList<Note>
	{
		#region CONSTRUCTION/LOAD

		 public NoteList()
        {
        }

        public NoteList(List<Note> items)
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
		public static NoteList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
            sortExpression = sortExpression.EnsureNotNull("ObservationID, Username, NoteTime");
			return new NoteList(NoteProvider.Instance().GetItemCollection(startRowIndex, maximumRows, sortExpression, filterExpression));
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NoteList GetItemCollection(int startRowIndex, int maximumRows, string sortExpression)
		{
			return GetItemCollection(startRowIndex, maximumRows, sortExpression, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NoteList GetItemCollection(int startRowIndex, int maximumRows)
		{
			return GetItemCollection(startRowIndex, maximumRows, string.Empty, string.Empty);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NoteList GetItemCollection()
		{
			return GetItemCollection(0, 0, string.Empty, string.Empty);
		}		
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Note GetItem(int id)
		{
			return NoteProvider.Instance().GetItem(id);
		}

		public static int GetCount()
        {
            return NoteProvider.Instance().GetCount(string.Empty);
        }

        public static int GetCount(string filterExpression)
        {
            return NoteProvider.Instance().GetCount(filterExpression);
        }

		[DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static Note AddItem(Note item)
        {
            Note nw = NoteProvider.Instance().AddItem(item);
            Audit(nw, ChangeLog.LogChangeType.create, nw.NoteID);
            RemoveCacheList();

            return nw;                
        }

		[DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateItem(Note item)
        {
            Note original = Get(item.NoteID);

            AuditUpdate(original, item, item.NoteID);

            NoteProvider.Instance().UpdateItem(item);

            RemoveCacheList();
            
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(Note item)
        {          
            DeleteItem(item.NoteID);
        }

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void DeleteItem(int id)
        {
            NoteProvider.Instance().DeleteItem(id);
            Audit(typeof(Note), ChangeLog.LogChangeType.delete, id);
            RemoveCacheList();                
        }

		#endregion

		#region PUBLIC METHODS

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static NoteList GetItemCollection(bool active)
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

            return GetItemCollection(0, 0, "ObservationID, Username, NoteTime", filter);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static NoteList GetActive()
        {
            List<Note> items = GetCacheList();

            if (items == null)
            {
                items = NoteList.GetItemCollection(true).ToList();
                AddToCache(items);
            }

            return new NoteList(items);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static NoteList GetActive(int observationId)
        {
            List<Note> items = GetActive();

            IEnumerable<Note> notes = items.Where(s => s.ObservationID == observationId);

            return new NoteList(notes.ToList());
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Note Get(int id)
        {
            NoteList items = GetActive();
            Note item = items.FirstOrDefault(s => s.NoteID == id);

            return item;
        }

		#endregion

	}   

}



