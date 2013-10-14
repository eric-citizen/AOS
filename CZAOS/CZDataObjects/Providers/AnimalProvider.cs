using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class AnimalProvider // SiteBaseProvider
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public AnimalProvider()
			{
			}   

		#endregion

		private static IAnimalProvider _instance = null;

		public static IAnimalProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
                _instance = new SQLAnimalProvider();
			}

			return _instance;
		}

	}    

	public interface IAnimalProvider
	{
        List<Animal> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        Animal GetItem(int id);
        Animal GetItemByZooID(string id);

        Animal AddItem(Animal item);
        void UpdateItem(Animal item);

		int GetCount(string filterExpression);

		void DeleteItem(int id);
	}

}


