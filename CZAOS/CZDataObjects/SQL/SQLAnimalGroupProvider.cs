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
	public class SQLAnimalGroupProvider : BaseSQLAccess, IAnimalGroupProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLAnimalGroupProvider()
			{
			}   			

		#endregion

		public List<AnimalGroup> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<AnimalGroup> items = new List<AnimalGroup>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_AnimalGroup_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						AnimalGroup item = new AnimalGroup(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public AnimalGroup GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                AnimalGroup item = null;

                base.AddParameter("@AnimalGroupId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_AnimalGroup_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new AnimalGroup(reader);							
					}
				}

                return item;
            }
		}

		public AnimalGroup AddItem(AnimalGroup item)
		{
			this.AddParameters(item); 

			AnimalGroup newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_AnimalGroup_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new AnimalGroup(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(AnimalGroup item)
		{
            base.AddParameter("@AnimalGroupId", item.AnimalGroupID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_AnimalGroup_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_AnimalGroup_GetCount", filterExpression);
		}

        //public void DeleteItem(int id)
        //{
        //    base.AddParameter("@Id", id);
        //    base.ExecuteNonQuery("czt_AnimalGroup_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        //}

        public void DeleteItemByObservationID(int observationid)
        {
            base.AddParameter("@ObservationId", observationid);
            base.ExecuteNonQuery("czt_AnimalGroup_DeleteByObservation", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(AnimalGroup item)
        {
            base.AddParameter("@GrpID",item.GrpID);
            base.AddParameter("@ObservationID",item.ObservationID);
            base.AddParameter("@GrpName",item.GrpName);
            base.AddParameter("@Deleted",item.Deleted);
            
        }

	}
}   

	       
	    
	   