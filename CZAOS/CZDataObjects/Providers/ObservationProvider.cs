using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ObservationProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ObservationProvider()
			{
			}   

		#endregion

		private static IObservationProvider _instance = null;

		public static IObservationProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLObservationProvider();
			}

			return _instance;
		}

	}    

	public interface IObservationProvider
	{
		List<Observation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        Observation GetItem(int id);

        Observation AddItem(Observation item);
        void UpdateItem(Observation item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


