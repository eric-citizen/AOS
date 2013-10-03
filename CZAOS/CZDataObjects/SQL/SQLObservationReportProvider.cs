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
	public class SQLObservationReportProvider : BaseSQLAccess, IObservationReportProvider 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLObservationReportProvider()
			{
			}   			

		#endregion

		public List<ObservationReport> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<ObservationReport> items = new List<ObservationReport>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Report_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						ObservationReport item = new ObservationReport(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public ObservationReport GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                ObservationReport item = null;

                base.AddParameter("@ReportID", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Report_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new ObservationReport(reader);							
					}
				}

                return item;
            }
		}

		public ObservationReport AddItem(ObservationReport item)
		{
			this.AddParameters(item); 

			ObservationReport newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Report_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new ObservationReport(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(ObservationReport item)
		{
            base.AddParameter("@ReportID", item.ReportID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Report_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Report_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@ReportID", id);
            base.ExecuteNonQuery("czt_Report_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(ObservationReport item)
        {
            base.AddParameter("@ObservationID", item.ObservationID);
            base.AddParameter("@ReportName", item.ReportName);
            base.AddParameter("@ReportLink", item.ReportLink);
            base.AddParameter("@Deleted", item.Deleted);
            
        }

	}
}   

	       
	    
	   