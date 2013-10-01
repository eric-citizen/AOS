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
	public class SQLUserProvider : BaseSQLAccess, IUserProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLUserProvider()
			{
			}   			

		#endregion

        public List<CZUser> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<CZUser> items = new List<CZUser>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_User_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						CZUser item = new CZUser(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

            public CZUser GetItem(Guid id)
		{
			lock(base.SyncLock_LOCK)
            {
                CZUser item = null;                

                base.AddParameter("@UserId", id);

                using (DbDataReader reader = base.ExecuteReader("czt_User_GetByID",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new CZUser(reader);							
					}
				}

                return item;
            }
		}

        public CZUser AddItem(CZUser item)
		{
			this.AddParameters(item); 

			CZUser newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_User_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new CZUser(reader, true);							
				}
			}
			
			return newItem;		
		}

        public void UpdateItem(CZUser item)
        {			
            this.AddParameters(item);
            base.ExecuteNonQuery("czt_User_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_User_GetCount", filterExpression);
		}

        //public void DeleteItem(Guid id)
        //{
        //    base.AddParameter("@UserId", id);
        //    base.ExecuteNonQuery("czt_User_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        //}

        private void AddParameters(CZUser item)
        {
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@DisplayName",item.DisplayName);
            base.AddParameter("@ExpirationDate",item.ExpirationDate);
            base.AddParameter("@UserType",item.UserType);
            base.AddParameter("@NewEmail",item.NewEmail);
            base.AddParameter("@CompEmail",item.CompEmail);
            base.AddParameter("@ObserveEmail",item.ObserveEmail);
            base.AddParameter("@WeekEmail",item.WeekEmail);
            base.AddParameter("@Active",item.Active);
            base.AddParameter("@UserId",item.UserId);            
        }

	}
}   

	       
	    
	   