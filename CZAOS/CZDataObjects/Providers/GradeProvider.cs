using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class GradeProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public GradeProvider()
			{
			}   

		#endregion

		private static IGradeProvider _instance = null;

		public static IGradeProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLGradeProvider();
			}

			return _instance;
		}

	}    

	public interface IGradeProvider
	{
		List<Grade> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		Grade GetItem(int id);

		Grade AddItem(Grade item);
		void UpdateItem(Grade item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


