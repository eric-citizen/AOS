using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class UserProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public UserProvider()
			{
			}   

		#endregion

		private static IUserProvider _instance = null;

		public static IUserProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLUserProvider();
			}

			return _instance;
		}

	}    

	public interface IUserProvider
	{
        List<CZUser> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        CZUser GetItem(Guid id);

        CZUser AddItem(CZUser item);
        void UpdateItem(CZUser item);

		int GetCount(string filterExpression);

        //void DeleteItem(Guid userId);
	}

}


