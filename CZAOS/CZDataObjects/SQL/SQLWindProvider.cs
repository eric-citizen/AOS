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
	public class SQLWindProvider : BaseSQLAccess, IWindProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLWindProvider()
			{
			}   			

		#endregion

		public List<Wind> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Wind> items = new List<Wind>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Wind_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Wind item = new Wind(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Wind GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Wind item = null;                

                base.AddParameter("@WindId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Wind_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Wind(reader);							
					}
				}

                return item;
            }
		}

		public Wind AddItem(Wind item)
		{
			this.AddParameters(item); 

			Wind newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Wind_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Wind(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Wind item)
		{
            base.AddParameter("@WindId", item.WindID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Wind_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Wind_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@WindId", id);
            base.ExecuteNonQuery("czt_Wind_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Wind item)
        {
            base.AddParameter("@Wind",item.Description);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   