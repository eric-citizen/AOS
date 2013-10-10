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
	public class SQLExhibitBehaviorProvider : BaseSQLAccess, IExhibitBehaviorProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLExhibitBehaviorProvider()
			{
			}   			

		#endregion

		public List<ExhibitBehavior> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<ExhibitBehavior> items = new List<ExhibitBehavior>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_ExhibitBehavior_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						ExhibitBehavior item = new ExhibitBehavior(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public ExhibitBehavior GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                ExhibitBehavior item = null;

                base.AddParameter("@ExhibitBehaviorId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_ExhibitBehavior_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new ExhibitBehavior(reader);							
					}
				}

                return item;
            }
		}

		public ExhibitBehavior AddItem(ExhibitBehavior item)
		{
			this.AddParameters(item); 

			ExhibitBehavior newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_ExhibitBehavior_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new ExhibitBehavior(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(ExhibitBehavior item)
		{
            base.AddParameter("@ExhibitBehaviorId", item.ExhibitBehaviorID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_ExhibitBehavior_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_ExhibitBehavior_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@ExhibitBehaviorId", id);
            base.ExecuteNonQuery("czt_ExhibitBehavior_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(ExhibitBehavior item)
        {
            base.AddParameter("@ExhibitID",item.ExhibitID);
            base.AddParameter("@BehaviorID",item.BehaviorID);
            base.AddParameter("@Active",item.Active);
            base.AddParameter("@BvrCatID", item.BvrCatID);
            
        }

	}
}   

	       
	    
	   