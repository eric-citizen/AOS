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
	public class SQLSchoolDistrictProvider : BaseSQLAccess, ISchoolDistrictProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLSchoolDistrictProvider()
			{
			}   			

		#endregion

		public List<SchoolDistrict> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<SchoolDistrict> items = new List<SchoolDistrict>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_SchoolDistrict_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						SchoolDistrict item = new SchoolDistrict(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public SchoolDistrict GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                SchoolDistrict item = null;                

                base.AddParameter("@DistrictId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_SchoolDistrict_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new SchoolDistrict(reader);							
					}
				}

                return item;
            }
		}

		public SchoolDistrict AddItem(SchoolDistrict item)
		{
			this.AddParameters(item); 

			SchoolDistrict newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_SchoolDistrict_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new SchoolDistrict(reader, true);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(SchoolDistrict item)
		{
            base.AddParameter("@DistrictId", item.DistrictID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_SchoolDistrict_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_SchoolDistrict_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@DistrictId", id);
            base.ExecuteNonQuery("czt_SchoolDistrict_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(SchoolDistrict item)
        {
            base.AddParameter("@District",item.District);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   