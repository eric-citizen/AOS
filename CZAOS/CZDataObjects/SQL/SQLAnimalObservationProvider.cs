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
	public class SQLAnimalObservationProvider : BaseSQLAccess, IAnimalObservationProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public SQLAnimalObservationProvider()
			{
			}   			

		#endregion

        public List<AnimalObservation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
                List<AnimalObservation> items = new List<AnimalObservation>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

                using (DbDataReader reader = base.ExecuteReader("czt_AnimalObservation_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
                        AnimalObservation item = new AnimalObservation(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

        public AnimalObservation AddItem(AnimalObservation item)
		{
			this.AddParameters(item);

            AnimalObservation newItem = null;

            using (DbDataReader reader = base.ExecuteReader("czt_AnimalObservation_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
                    newItem = new AnimalObservation(reader, true);							
				}
			}
			
			return newItem;		
		}

        public void UpdateItem(AnimalObservation item)
		{
            base.AddParameter("@AnimalObservationID", item.AnimalObservationID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_AnimalObservation_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

        public void DeleteByObservation(int observationId)
        {
            base.AddParameter("@observationId", observationId);
            base.ExecuteNonQuery("czt_AnimalObservation_DeleteByObservation", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        private void AddParameters(AnimalObservation item)
        {
            base.AddParameter("@ObservationID", item.ObservationID);
            base.AddParameter("@GrpID", item.GroupID);
            base.AddParameter("@AnimalID", item.AnimalID);
            base.AddParameter("@Deleted", item.Deleted);
            
        }

	}
}   

	       
	    
	   