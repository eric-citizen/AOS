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
	public class SQLExhibitLocationProvider : BaseSQLAccess, IExhibitLocationProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLExhibitLocationProvider()
			{
			}   			

		#endregion

		public List<ExhibitLocation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<ExhibitLocation> items = new List<ExhibitLocation>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_ExhibitLocation_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						ExhibitLocation item = new ExhibitLocation(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public ExhibitLocation GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                ExhibitLocation item = null;

                base.AddParameter("@ExhibitLocationId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_ExhibitLocation_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new ExhibitLocation(reader);							
					}
				}

                return item;
            }
		}

		public ExhibitLocation AddItem(ExhibitLocation item)
		{
			this.AddParameters(item); 

			ExhibitLocation newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_ExhibitLocation_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new ExhibitLocation(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(ExhibitLocation item)
		{
            base.AddParameter("@ExhibitLocationId", item.ExhibitLocationID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_ExhibitLocation_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_ExhibitLocation_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@ExhibitLocationId", id);
            base.ExecuteNonQuery("czt_ExhibitLocation_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(ExhibitLocation item)
        {
            base.AddParameter("@ExhibitID",item.ExhibitID);
            base.AddParameter("@LocationID",item.LocationID);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   