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
	public class SQLChangeLogProvider : BaseSQLAccess, IChangeLogProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLChangeLogProvider()
			{
			}   			

		#endregion

		public List<ChangeLog> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<ChangeLog> items = new List<ChangeLog>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_ChangeLog_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						ChangeLog item = new ChangeLog(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public ChangeLog GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                ChangeLog item = null;                

                base.AddParameter("@Id", id);

				using (DbDataReader reader = base.ExecuteReader("czt_ChangeLog_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new ChangeLog(reader);							
					}
				}

                return item;
            }
		}

		public ChangeLog AddItem(ChangeLog item)
		{
			this.AddParameters(item); 

			ChangeLog newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_ChangeLog_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new ChangeLog(reader);							
				}
			}
			
			return newItem;		
		}

        //public void UpdateItem(ChangeLog item)
        //{
        //    base.AddParameter("@Id", item.ID );
        //    this.AddParameters(item);

        //    base.ExecuteNonQuery("czt_ChangeLog_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        //}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_ChangeLog_GetCount", filterExpression);
		}

        public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_ChangeLog_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(ChangeLog item)
        {            
            base.AddParameter("@UserID",item.UserID);
            base.AddParameter("@UserDisplayName",item.UserDisplayName);
            base.AddParameter("@ChangeType",item.ChangeType.ToString());
            base.AddParameter("@Identifier",item.Identifier);
            base.AddParameter("@Changes",item.Changes);
            
        }

	}
}   

	       
	    
	   