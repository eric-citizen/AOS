using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class WeatherConditionProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public WeatherConditionProvider()
			{
			}   

		#endregion

		private static IWeatherConditionProvider _instance = null;

		public static IWeatherConditionProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLWeatherConditionProvider();
			}

			return _instance;
		}

	}    

	public interface IWeatherConditionProvider
	{
		List<WeatherCondition> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		WeatherCondition GetItem(int id);

		WeatherCondition AddItem(WeatherCondition item);
		void UpdateItem(WeatherCondition item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


