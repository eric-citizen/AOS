using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class AnimalRegionProvider 
	{
		#region CONSTRUCTORS/DESTRUCTORS

			public AnimalRegionProvider()
			{
			}   

		#endregion

		private static IAnimalRegionProvider _instance = null;

		public static IAnimalRegionProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
				_instance = new SQLAnimalRegionProvider();
			}

			return _instance;
		}

	}    

	public interface IAnimalRegionProvider
	{
        List<AnimalRegion> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);
        AnimalRegion GetItem(string animalRegionCode);
        AnimalRegion AddItem(AnimalRegion item);
        void UpdateItem(AnimalRegion item);
		int GetCount(string filterExpression);

        void DeleteItem(string id);
	}

}


