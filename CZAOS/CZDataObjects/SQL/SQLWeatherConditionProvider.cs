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
	public class SQLWeatherConditionProvider : BaseSQLAccess, IWeatherConditionProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SQLWeatherConditionProvider()
			{
			}   			

		#endregion

		public List<WeatherCondition> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			lock(base.SyncLock_LOCK)
            {
				List<WeatherCondition> items = new List<WeatherCondition>();
                //SqlDataReader reader = null;

                sortExpression = sortExpression.EnsureNotNull();
                filterExpression = filterExpression.EnsureNotNull();

                base.AddParameter("@startRowIndex", startRowIndex);
                base.AddParameter("@maximumRows", maximumRows);
                base.AddParameter("@sortExpression", sortExpression);
                base.AddParameter("@filterExpression", filterExpression);

				using (DbDataReader reader = base.ExecuteReader("czt_WeatherCondition_GetPagedList",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					while (reader.Read())
					{
						WeatherCondition item = new WeatherCondition(reader);
						items.Add(item);
					}
				}

                return items;
            }
		}

		public WeatherCondition GetItem(int id)
		{
			lock(base.SyncLock_LOCK)
            {
                WeatherCondition item = null;                

                base.AddParameter("@WeatherId", id);

				using (DbDataReader reader = base.ExecuteReader("czt_WeatherCondition_Get",
                                System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
				{
					if (reader.Read())
					{
						item = new WeatherCondition(reader);							
					}
				}

                return item;
            }
		}

		public WeatherCondition AddItem(WeatherCondition item)
		{
			this.AddParameters(item); 

			WeatherCondition newItem = null;

			using (DbDataReader reader = base.ExecuteReader("czt_WeatherCondition_Insert",
                            System.Data.CommandType.StoredProcedure, ConnectionState.CloseOnExit))
			{
				if (reader.Read())
				{
					newItem = new WeatherCondition(reader);							
				}
			}
			
			return newItem;		
		}

		public void UpdateItem(WeatherCondition item)
		{
            base.AddParameter("@WeatherId", item.WeatherID);
            this.AddParameters(item);

            base.ExecuteNonQuery("czt_WeatherCondition_Update", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
		}

		public int GetCount(string filterExpression)
		{
			filterExpression = filterExpression.EnsureNotNull();
			return base.GetFilteredCount("czt_WeatherCondition_GetCount", filterExpression);
		}

		public void DeleteItem(int id)
        {
            base.AddParameter("@WeatherId", id);
            base.ExecuteNonQuery("czt_WeatherCondition_Delete", CommandType.StoredProcedure, ConnectionState.CloseOnExit);
        }

		private void AddParameters(WeatherCondition item)
        {
            base.AddParameter("@Weather",item.Weather);
            base.AddParameter("@Active",item.Active);
            
        }

	}
}   

	       
	    
	   