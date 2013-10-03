using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using KT.Extensions;
using CZDataObjects.Extensions;

namespace CZDataObjects
{
	public class SQLBehaviorCategoryProvider : BaseSQLAccess, IBehaviorCategoryProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLBehaviorCategoryProvider()
			{
			}   			

		#endregion

		public List<BehaviorCategory> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<BehaviorCategory> items = new List<BehaviorCategory>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_BehaviorCategory_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						BehaviorCategory item = new BehaviorCategory(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public BehaviorCategory GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                BehaviorCategory item = null;                

                base.AddParameter("@BvrCatId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_BehaviorCategory_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new BehaviorCategory(reader);							
					}
				}

                return item;
            }
		}

		public BehaviorCategory AddItem(BehaviorCategory item)
		{
			this.AddParameters(item); 

			BehaviorCategory newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_BehaviorCategory_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new BehaviorCategory(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(BehaviorCategory item)
		{
            base.AddParameter("@BvrCatId", item.BvrCatID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_BehaviorCategory_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_BehaviorCategory_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@BvrCatId", id);
            base.ExecuteNonQuery("czt_BehaviorCategory_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
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
                sbrSQL.AppendFormat("UPDATE czt_BehaviorCategory ");
                sbrSQL.AppendFormat("SET SORTORDER = {0} ", item.SortOrder);
                sbrSQL.AppendFormat("WHERE BvrCatId = {0} ; ", item.SortItemID);
            }

            base.ExecuteNonQuery(sbrSQL.ToString(), CommandType.Text);
        }

		private void AddParameters(BehaviorCategory item)
        {
            base.AddParameter("@BvrCat",item.BvrCat);
            base.AddParameter("@BvrCatCode",item.BvrCatCode);
            base.AddParameter("@Description",item.Description);
            base.AddParameter("@MaskAma",item.MaskAma.ToYesNoString());
            base.AddParameter("@MaskProf", item.MaskProf.ToYesNoString());
            base.AddParameter("@SortOrder",item.SortOrder);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   