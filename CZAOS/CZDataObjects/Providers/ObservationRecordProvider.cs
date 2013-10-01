using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ObservationRecordProvider 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ObservationRecordProvider()
			{
			}   

		#endregion

		private static IObservationRecordProvider _instance = null;

		public static IObservationRecordProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{				
                _instance = new SQLObservationRecordProvider();
			}

			return _instance;
		}

	}    

	public interface IObservationRecordProvider
	{
		List<ObservationRecord> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		ObservationRecord GetItem(int id);

		ObservationRecord AddItem(ObservationRecord item);
		void UpdateItem(ObservationRecord item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void DeleteByObservation(int observationId);
	}

}


