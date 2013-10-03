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
	public class SQLCrowdProvider : BaseSQLAccess, ICrowdProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLCrowdProvider()
			{
			}   			

		#endregion

		public List<Crowd> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Crowd> items = new List<Crowd>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Crowd_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Crowd item = new Crowd(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Crowd GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Crowd item = null;                

                base.AddParameter("@CrowdId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Crowd_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
                        item = new Crowd(reader);							
					}
				}

                return item;
            }
		}

		public Crowd AddItem(Crowd item)
		{
			this.AddParameters(item); 

			Crowd newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Crowd_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Crowd(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Crowd item)
		{
            base.AddParameter("@CrowdId", item.CrowdID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Crowd_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Crowd_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@CrowdId", id);
            base.ExecuteNonQuery("czt_Crowd_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Crowd item)
        {
            base.AddParameter("@Crowd",item.CrowdName);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   