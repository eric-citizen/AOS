using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class UserRegionProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public UserRegionProvider()
			{
			}   

		#endregion

		private static IUserRegionProvider _instance = null;

		public static IUserRegionProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLUserRegionProvider();
			}

			return _instance;
		}

	}    

	public interface IUserRegionProvider
	{
		List<UserRegion> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		UserRegion GetItem(int id);

		UserRegion AddItem(UserRegion item);
		void UpdateItem(UserRegion item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


