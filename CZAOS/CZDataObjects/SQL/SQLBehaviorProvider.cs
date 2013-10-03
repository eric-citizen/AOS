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
	public class SQLBehaviorProvider : BaseSQLAccess, IBehaviorProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLBehaviorProvider()
			{
			}   			

		#endregion

		public List<Behavior> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Behavior> items = new List<Behavior>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Behavior_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Behavior item = new Behavior(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Behavior GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Behavior item = null;

                base.AddParameter("@BehaviorID", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Behavior_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Behavior(reader);							
					}
				}

                return item;
            }
		}

		public Behavior AddItem(Behavior item)
		{
			this.AddParameters(item); 

			Behavior newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Behavior_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Behavior(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Behavior item)
		{
            base.AddParameter("@BehaviorID", item.BehaviorID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Behavior_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_Behavior_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@BehaviorID", id);
            base.ExecuteNonQuery("czt_Behavior_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        public void UpdateSort(List<Interfaces.ISortable> items)
        {
            if (items.Count == 0)
            {
                return;
            }

            System.Text.StringBuilder sbrSQL = new System.Text.StringBuilder();

            foreach (Interfaces.ISortable item in items)
            {
                sbrSQL.AppendFormat("UPDATE czt_Behavior ");
                sbrSQL.AppendFormat("SET SORTORDER = {0} ", item.SortOrder);
                sbrSQL.AppendFormat("WHERE BehaviorId = {0} ; ", item.SortItemID);
            }

            base.ExecuteNonQuery(sbrSQL.ToString(), CommandType.Text);
        }

		private void AddParameters(Behavior item)
        {
            base.AddParameter("@Behavior",item.BehaviorName);
            base.AddParameter("@BvrCatID",item.BvrCatID);
            base.AddParameter("@BehaviorCode",item.BehaviorCode);
            base.AddParameter("@Description",item.Description);
            base.AddParameter("@SortOrder",item.SortOrder);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   