using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ChangeLogProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ChangeLogProvider()
			{
			}   

		#endregion

		private static IChangeLogProvider _instance = null;

		public static IChangeLogProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLChangeLogProvider();
			}

			return _instance;
		}

	}    

	public interface IChangeLogProvider
	{
		List<ChangeLog> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		ChangeLog GetItem(int id);
		ChangeLog AddItem(ChangeLog item);		
		void DeleteItem(int id);		
		int GetCount(string filterExpression);		 
	}

}


