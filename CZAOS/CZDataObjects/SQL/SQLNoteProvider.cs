using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using KT.Extensions;


namespace CZDataObjects
{
	public class SQLNoteProvider : BaseSQLAccess, INoteProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLNoteProvider()
			{
			}   			

		#endregion

		public List<Note> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Note> items = new List<Note>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
				base.AddParameter("@maximumRows", maximumRows);
				base.AddParameter("@sortExpression", sortExpression);
				base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Note_GetPagedList",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Note item = new Note(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Note GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Note item = null;

                base.AddParameter("@NoteId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Note_Get",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Note(reader);							
					}
				}                

                return item;
            }
		}

        public Note AddItem(Note item)
		{
			this.AddParameters(item);            

			Note newItem = null;
			using (DbDataReader reader = base.ExecuteReader("czt_Note_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Note(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Note item)
		{
            base.AddParameter("@NoteId", item.NoteID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Note_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_Note_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@NoteId", id);
            base.ExecuteNonQuery("czt_Note_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Note item)
        {
            base.AddParameter("@ObservationID",item.ObservationID);
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@NoteTime",item.NoteTime);
            base.AddParameter("@Note",item.NoteText);
            base.AddParameter("@Deleted",item.Deleted);            
        }

	}
}   

	       
	    
	   