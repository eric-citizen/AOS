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
	public class SQLAdminNavigationProvider : BaseSQLAccess, IAdminNavigationProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLAdminNavigationProvider()
			{
			}   			

		#endregion

		public List<AdminNavigation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<AdminNavigation> items = new List<AdminNavigation>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_AdminNavigation_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						AdminNavigation item = new AdminNavigation(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public AdminNavigation GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                AdminNavigation item = null;                

                base.AddParameter("@Id", id);

				using (DbDataReader reader = base.ExecuteReader("czt_AdminNavigation_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new AdminNavigation(reader);							
					}
				}

                return item;
            }
		}

		public AdminNavigation AddItem(AdminNavigation item)
		{
			this.AddParameters(item); 

			AdminNavigation newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_AdminNavigation_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new AdminNavigation(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(AdminNavigation item)
		{
			base.AddParameter("@Id", item.ID );
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_AdminNavigation_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_AdminNavigation_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_AdminNavigation_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(AdminNavigation item)
        {
            base.AddParameter("@Folder",item.Folder);
            base.AddParameter("@Roles",item.Roles);
            base.AddParameter("@NavText", item.NavText);
        }

        ////czt_AdminNavigation_Init
        public void InitNav()
        {           
            base.ExecuteNonQuery("czt_AdminNavigation_Init", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }
        
	}
}   

	       
	    
	   