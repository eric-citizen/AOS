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
	public class SQLAnimalRegionProvider: BaseSQLAccess, IAnimalRegionProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLAnimalRegionProvider()
			{
			}   			

		#endregion

            public List<AnimalRegion> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<AnimalRegion> items = new List<AnimalRegion>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_AnimalRegion_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						AnimalRegion item = new AnimalRegion(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

            public AnimalRegion GetItem(string animalRegionCode)
		{
			lock(base.SyncLock_LOCK)
            {
                AnimalRegion item = null;
                animalRegionCode.EnsureNotNull();

                base.AddParameter("@AnimalRegionCode", animalRegionCode);

				using (DbDataReader reader = base.ExecuteReader("czt_AnimalRegion_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new AnimalRegion(reader);							
					}
				}

                return item;
            }
		}

        public AnimalRegion AddItem(AnimalRegion item)
		{
			this.AddParameters(item); 

			AnimalRegion newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_AnimalRegion_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new AnimalRegion(reader, true);							
				}
			}
			
			return newItem;		
		}

        public void UpdateItem(AnimalRegion item)
		{
            
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_AnimalRegion_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
            filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_AnimalRegion_GetCount", filterExpression);
		}

        public void DeleteItem(string id)
        {
            base.AddParameter("@AnimalRegionCode", id);
            base.ExecuteNonQuery("czt_AnimalRegion_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(AnimalRegion item)
        {
            base.AddParameter("@AnimalRegionCode", item.AnimalRegionCode);
            base.AddParameter("@AnimalRegion", item.AnimalRegionName);
            base.AddParameter("@RegionName",item.RegionName);
            base.AddParameter("@Active",item.Active);            
        }

	}
}   

	       
	    
	   