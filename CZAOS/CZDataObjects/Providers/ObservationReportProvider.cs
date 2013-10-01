using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ObservationReportProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ObservationReportProvider()
			{
			}   

		#endregion

		private static IObservationReportProvider _instance = null;

		public static IObservationReportProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLObservationReportProvider();
			}

			return _instance;
		}

	}    

	public interface IObservationReportProvider
	{
		List<ObservationReport> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		ObservationReport GetItem(int id);

		ObservationReport AddItem(ObservationReport item);
		void UpdateItem(ObservationReport item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


