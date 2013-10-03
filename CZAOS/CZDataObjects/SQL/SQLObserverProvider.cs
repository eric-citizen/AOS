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
	public class SQLObserverProvider : BaseSQLAccess, IObserverProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLObserverProvider()
			{
			}   			

		#endregion

		public List<Observer> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Observer> items = new List<Observer>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Observer_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Observer item = new Observer(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Observer GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Observer item = null;

                base.AddParameter("@observerId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Observer_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Observer(reader);							
					}
				}

                return item;
            }
		}

		public Observer AddItem(Observer item)
		{
			this.AddParameters(item); 

			Observer newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Observer_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Observer(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Observer item)
		{
            base.AddParameter("@observerId", item.ObserverID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Observer_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Observer_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@observerId", id);
            base.ExecuteNonQuery("czt_Observer_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        public void DeleteByObservation(int observationId)
        {
            base.AddParameter("@observationId", observationId);
            base.ExecuteNonQuery("czt_Observer_DeleteByObservation", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }
        
		private void AddParameters(Observer item)
        {
            base.AddParameter("@ObservationID",item.ObservationID);
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@Locked",item.Locked);
            base.AddParameter("@Deleted",item.Deleted);
            
        }

	}
}   

	       
	    
	   