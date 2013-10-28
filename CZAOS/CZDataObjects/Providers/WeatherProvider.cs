using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class WeatherProvider 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public WeatherProvider()
			{
			}   

		#endregion

		private static IWeatherProvider _instance = null;

		public static IWeatherProvider Instance()
		{
			if(_instance == null)
			{
                //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
                _instance = new SQLWeatherProvider();
			}

			return _instance;
		}

	}    

	public interface IWeatherProvider
	{
		List<Weather> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Weather GetItem(int id);

		Weather AddItem(Weather item);
		void UpdateItem(Weather item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void DeleteByObservationID(int observationId);
	}

}


