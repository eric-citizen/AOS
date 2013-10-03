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
	public class SQLLocationProvider : BaseSQLAccess, ILocationProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLLocationProvider()
			{
			}   			

		#endregion

		public List<Location> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Location> items = new List<Location>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Location_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Location item = new Location(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Location GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Location item = null;                

                base.AddParameter("@LocationId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Location_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Location(reader);							
					}
				}

                return item;
            }
		}

		public Location AddItem(Location item)
		{
			this.AddParameters(item); 

			Location newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Location_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Location(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Location item)
		{
            base.AddParameter("@LocationId", item.LocationID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Location_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
            filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_Location_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@LocationId", id);
            base.ExecuteNonQuery("czt_Location_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Location item)
        {
            base.AddParameter("@Description",item.Description);
            base.AddParameter("@LocationCode",item.LocationCode);
            base.AddParameter("@MaskAma",item.MaskAma.ToYesNoString());
            base.AddParameter("@MaskProf",item.MaskProf.ToYesNoString());
            base.AddParameter("@SortOrder",item.SortOrder);
            base.AddParameter("@Active",item.Active);
            
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
                sbrSQL.AppendFormat("UPDATE czt_Location ");
                sbrSQL.AppendFormat("SET SORTORDER = {0} ", item.SortOrder);
                sbrSQL.AppendFormat("WHERE locationId = {0} ; ", item.SortItemID);
            }

            base.ExecuteNonQuery(sbrSQL.ToString(), CommandType.Text);
        }

	}
}   

	       
	    
	   