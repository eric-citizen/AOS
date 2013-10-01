using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class LocationProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public LocationProvider()
			{
			}   

		#endregion

		private static ILocationProvider _instance = null;

		public static ILocationProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLLocationProvider();
			}

			return _instance;
		}

	}    

	public interface ILocationProvider
	{
		List<Location> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Location GetItem(int id);

		Location AddItem(Location item);
		void UpdateItem(Location item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void UpdateSort(List<Interfaces.ISortable> items);
	}

}


