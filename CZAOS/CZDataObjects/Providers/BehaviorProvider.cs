using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class BehaviorProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public BehaviorProvider()
			{
			}   

		#endregion

		private static IBehaviorProvider _instance = null;

		public static IBehaviorProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLBehaviorProvider();
			}

			return _instance;
		}

	}    

	public interface IBehaviorProvider
	{
		List<Behavior> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Behavior GetItem(int id);

		Behavior AddItem(Behavior item);
		void UpdateItem(Behavior item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);

        void UpdateSort(List<Interfaces.ISortable> items);
	}

}


