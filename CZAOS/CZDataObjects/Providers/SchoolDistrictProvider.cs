using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class SchoolDistrictProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public SchoolDistrictProvider()
			{
			}   

		#endregion

		private static ISchoolDistrictProvider _instance = null;

		public static ISchoolDistrictProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLSchoolDistrictProvider();
			}

			return _instance;
		}

	}    

	public interface ISchoolDistrictProvider
	{
		List<SchoolDistrict> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		SchoolDistrict GetItem(int id);

		SchoolDistrict AddItem(SchoolDistrict item);
		void UpdateItem(SchoolDistrict item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


