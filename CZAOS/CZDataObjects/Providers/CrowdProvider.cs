using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class CrowdProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public CrowdProvider()
			{
			}   

		#endregion

		private static ICrowdProvider _instance = null;

		public static ICrowdProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLCrowdProvider();
			}

			return _instance;
		}

	}    

	public interface ICrowdProvider
	{
		List<Crowd> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Crowd GetItem(int id);

		Crowd AddItem(Crowd item);
		void UpdateItem(Crowd item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


