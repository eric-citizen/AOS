using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class SchoolProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SchoolProvider()
			{
			}   

		#endregion

		private static ISchoolProvider _instance = null;

		public static ISchoolProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLSchoolProvider();
			}

			return _instance;
		}

	}    

	public interface ISchoolProvider
	{
        List<School> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        School GetItem(int id);

        School AddItem(School item);
        void UpdateItem(School item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


