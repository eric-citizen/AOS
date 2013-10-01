using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ExhibitLocationProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ExhibitLocationProvider()
			{
			}   

		#endregion

		private static IExhibitLocationProvider _instance = null;

		public static IExhibitLocationProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLExhibitLocationProvider();
			}

			return _instance;
		}

	}    

	public interface IExhibitLocationProvider
	{
		List<ExhibitLocation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		ExhibitLocation GetItem(int id);

		ExhibitLocation AddItem(ExhibitLocation item);
		void UpdateItem(ExhibitLocation item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


