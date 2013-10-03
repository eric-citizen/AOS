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
	public class SQLExhibitProvider : BaseSQLAccess, IExhibitProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLExhibitProvider()
			{
			}   			

		#endregion

		public List<Exhibit> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Exhibit> items = new List<Exhibit>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Exhibit_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Exhibit item = new Exhibit(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Exhibit GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Exhibit item = null;

                base.AddParameter("@ExhibitId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Exhibit_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Exhibit(reader);							
					}
				}

                return item;
            }
		}

		public Exhibit AddItem(Exhibit item)
		{
			this.AddParameters(item); 

			Exhibit newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_Exhibit_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Exhibit(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Exhibit item)
		{
            base.AddParameter("@ExhibitId", item.ExhibitID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Exhibit_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_Exhibit_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@ExhibitId", id);
            base.ExecuteNonQuery("czt_Exhibit_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Exhibit item)
        {
            base.AddParameter("@AnimalRegionCode",item.AnimalRegionCode);
            base.AddParameter("@Exhibit",item.ExhibitName);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   