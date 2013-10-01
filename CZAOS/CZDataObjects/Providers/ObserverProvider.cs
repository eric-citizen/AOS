using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ObserverProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ObserverProvider()
			{
			}   

		#endregion

		private static IObserverProvider _instance = null;

		public static IObserverProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLObserverProvider();
			}

			return _instance;
		}

	}    

	public interface IObserverProvider
	{
		List<Observer> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Observer GetItem(int id);

		Observer AddItem(Observer item);
		void UpdateItem(Observer item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void DeleteByObservation(int observationId);
	}

}


