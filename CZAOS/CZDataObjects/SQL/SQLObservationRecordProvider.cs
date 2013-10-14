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
	public class SQLObservationRecordProvider : BaseSQLAccess, IObservationRecordProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLObservationRecordProvider()
			{
			}   			

		#endregion

		public List<ObservationRecord> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<ObservationRecord> items = new List<ObservationRecord>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
				base.AddParameter("@maximumRows", maximumRows);
				base.AddParameter("@sortExpression", sortExpression);
				base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_ObservationRecord_GetPagedList",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						ObservationRecord item = new ObservationRecord(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public ObservationRecord GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                ObservationRecord item = null;                

                base.AddParameter("@ObservationRecordID", id);

				using (DbDataReader reader = base.ExecuteReader("czt_ObservationRecord_Get",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new ObservationRecord(reader);							
					}
				}                

                return item;
            }
		}

        public ObservationRecord AddItem(ObservationRecord item)
		{
			this.AddParameters(item);            

			ObservationRecord newItem = null;
			using (DbDataReader reader = base.ExecuteReader("czt_ObservationRecord_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new ObservationRecord(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(ObservationRecord item)
		{
			base.AddParameter("@ObservationRecordID", item.ObservationRecordID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_ObservationRecord_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_ObservationRecord_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@ObservationRecordID", id);
            base.ExecuteNonQuery("czt_ObservationRecord_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }
        public void DeleteByObservation(int observationId)
        {
            base.AddParameter("@Observation", observationId);
            base.ExecuteNonQuery("czt_ObservationRecord_DeleteByObservation", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(ObservationRecord item)
        {
            base.AddParameter("@ObservationID",item.ObservationID);
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@ZooID",item.ZooID);
            base.AddParameter("@BvrCat",item.BvrCat);
            base.AddParameter("@BvrCatCode",item.BvrCatCode);
            base.AddParameter("@Behavior",item.Behavior);
            base.AddParameter("@BehaviorCode",item.BehaviorCode);
            base.AddParameter("@LocationID",item.LocationID);
            base.AddParameter("@ObserverTime",item.ObserverTime);
            base.AddParameter("@Deleted",item.Deleted);
            base.AddParameter("@Flagged",item.Flagged);
            
        }

	}
}   

	       
	    
	   