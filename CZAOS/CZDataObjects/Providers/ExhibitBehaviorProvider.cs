using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class ExhibitBehaviorProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public ExhibitBehaviorProvider()
			{
			}   

		#endregion

		private static IExhibitBehaviorProvider _instance = null;

		public static IExhibitBehaviorProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLExhibitBehaviorProvider();
			}

			return _instance;
		}

	}    

	public interface IExhibitBehaviorProvider
	{
		List<ExhibitBehavior> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		ExhibitBehavior GetItem(int id);

		ExhibitBehavior AddItem(ExhibitBehavior item);
		void UpdateItem(ExhibitBehavior item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


