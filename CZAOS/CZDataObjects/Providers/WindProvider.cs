using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class WindProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public WindProvider()
			{
			}   

		#endregion

		private static IWindProvider _instance = null;

		public static IWindProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLWindProvider();
			}

			return _instance;
		}

	}    

	public interface IWindProvider
	{
		List<Wind> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Wind GetItem(int id);

		Wind AddItem(Wind item);
		void UpdateItem(Wind item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


