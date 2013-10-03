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
	public class SQLGradeProvider : BaseSQLAccess, IGradeProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLGradeProvider()
			{
			}   			

		#endregion

		public List<Grade> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Grade> items = new List<Grade>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Grade_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Grade item = new Grade(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Grade GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Grade item = null;

                base.AddParameter("@GradeId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Grade_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Grade(reader);							
					}
				}

                return item;
            }
		}

		public Grade AddItem(Grade item)
		{
			this.AddParameters(item); 

			Grade newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Grade_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Grade(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Grade item)
		{
            base.AddParameter("@GradeId", item.GradeID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Grade_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Grade_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@GradeId", id);
            base.ExecuteNonQuery("czt_Grade_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Grade item)
        {
            base.AddParameter("@Grade",item.GradeName);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   