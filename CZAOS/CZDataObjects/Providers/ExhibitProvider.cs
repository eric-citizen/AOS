using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ExhibitProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ExhibitProvider()
			{
			}   

		#endregion

		private static IExhibitProvider _instance = null;

		public static IExhibitProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLExhibitProvider();
			}

			return _instance;
		}

	}    

	public interface IExhibitProvider
	{
		List<Exhibit> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Exhibit GetItem(int id);

		Exhibit AddItem(Exhibit item);
		void UpdateItem(Exhibit item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


