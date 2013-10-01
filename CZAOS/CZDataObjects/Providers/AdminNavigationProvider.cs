using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class AdminNavigationProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public AdminNavigationProvider()
			{
			}   

		#endregion

		private static IAdminNavigationProvider _instance = null;

		public static IAdminNavigationProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLAdminNavigationProvider(); 
			}

			return _instance;
		}

	}    

	public interface IAdminNavigationProvider
	{
		List<AdminNavigation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		AdminNavigation GetItem(int id);

		AdminNavigation AddItem(AdminNavigation item);
		void UpdateItem(AdminNavigation item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void InitNav();
        

	}

}


