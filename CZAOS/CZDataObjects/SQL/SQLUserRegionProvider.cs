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
	public class SQLUserRegionProvider : BaseSQLAccess, IUserRegionProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLUserRegionProvider()
			{
			}   			

		#endregion

		public List<UserRegion> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<UserRegion> items = new List<UserRegion>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_UserRegion_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						UserRegion item = new UserRegion(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public UserRegion GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                UserRegion item = null;

                base.AddParameter("@UserRegionID", id);

				using (DbDataReader reader = base.ExecuteReader("czt_UserRegion_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new UserRegion(reader);							
					}
				}

                return item;
            }
		}

		public UserRegion AddItem(UserRegion item)
		{
			this.AddParameters(item); 

			UserRegion newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_UserRegion_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new UserRegion(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(UserRegion item)
		{
            base.AddParameter("@UserRegionID", item.UserRegionID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_UserRegion_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_UserRegion_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@UserRegionID", id);
            base.ExecuteNonQuery("czt_UserRegion_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(UserRegion item)
        {
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@AnimalRegionCode",item.AnimalRegionCode);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   