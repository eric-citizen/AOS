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
	public class SQLWeatherProvider : BaseSQLAccess, IWeatherProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLWeatherProvider()
			{
			}   			

		#endregion

		public List<Weather> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<Weather> items = new List<Weather>();
                //SqlDataReader reader = null;

                sortExpression.EnsureNotNull();
                filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
				base.AddParameter("@maximumRows", maximumRows);
				base.AddParameter("@sortExpression", sortExpression);
				base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_Weather_GetPagedList",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						Weather item = new Weather(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public Weather GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                Weather item = null;                

                base.AddParameter("@ObservationId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_Weather_Get",
								System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new Weather(reader);							
					}
				}                

                return item;
            }
		}

		public Weather AddItem(Weather item)
		{
			this.AddParameters(item);            

			Weather newItem = null;
			using (DbDataReader reader = base.ExecuteReader("czt_Weather_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new Weather(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(Weather item)
		{
			base.AddParameter("@Id", item.ID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_Weather_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			return base.GetFilteredCount("czt_Weather_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@Id", id);
            base.ExecuteNonQuery("czt_Weather_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

        public void DeleteByObservationID(int observationId)
        {
            base.AddParameter("@observationId", observationId);
            base.ExecuteNonQuery("czt_Weather_DeleteByObservationID", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(Weather item)
        {
            base.AddParameter("@ObservationID",item.ObservationID);
            base.AddParameter("@Username",item.Username);
            base.AddParameter("@WeatherConditionID",item.WeatherConditionID);
            base.AddParameter("@Temperature",item.Temperature);
            base.AddParameter("@WindID",item.WindID);
            base.AddParameter("@CrowdID",item.CrowdID);
            base.AddParameter("@WeatherTime",item.WeatherTime);
            base.AddParameter("@Deleted",item.Deleted);
            base.AddParameter("@Flagged",item.Flagged);
            
        }

	}
}   

	       
	    
	   