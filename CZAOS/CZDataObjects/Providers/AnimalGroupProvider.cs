using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class AnimalGroupProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public AnimalGroupProvider()
			{
			}   

		#endregion

		private static IAnimalGroupProvider _instance = null;

		public static IAnimalGroupProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLAnimalGroupProvider();
			}

			return _instance;
		}

	}    

	public interface IAnimalGroupProvider
	{
		List<AnimalGroup> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
		AnimalGroup GetItem(int id);

		AnimalGroup AddItem(AnimalGroup item);
		void UpdateItem(AnimalGroup item);

		int GetCount(string filterExpression);

		//void DeleteItem(int id);
        void DeleteItemByObservationID(int id);
	}

}


