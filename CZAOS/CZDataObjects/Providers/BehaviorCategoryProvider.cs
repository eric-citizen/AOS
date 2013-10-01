using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class BehaviorCategoryProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public BehaviorCategoryProvider()
			{
			}   

		#endregion

		private static IBehaviorCategoryProvider _instance = null;

		public static IBehaviorCategoryProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLBehaviorCategoryProvider();
			}

			return _instance;
		}

	}    

	public interface IBehaviorCategoryProvider
	{
		List<BehaviorCategory> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		BehaviorCategory GetItem(int id);

		BehaviorCategory AddItem(BehaviorCategory item);
		void UpdateItem(BehaviorCategory item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
        void UpdateSort(List<Interfaces.ISortable> items);
	}

}


