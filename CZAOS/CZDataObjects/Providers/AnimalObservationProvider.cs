using System;
using System.Collections.Generic;


namespace CZDataObjects
{
	public sealed class AnimalObservationProvider // 
	{
		#region CONSTRUCTORS/DESTRUCTORS

        public AnimalObservationProvider()
			{
			}   

		#endregion

        private static IAnimalObservationProvider _instance = null;

        public static IAnimalObservationProvider Instance()
		{
			if(_instance == null) //short cut - if you really want the provider model, then specify the provider by config value here - use Activator.CreateInstance
			{
                _instance = new SQLAnimalObservationProvider();
			}

			return _instance;
		}

	}

    public interface IAnimalObservationProvider
	{
        List<AnimalObservation> GetItemCollection(int startRowIndex, int maximumRows, string sortExpression, string filterExpression);

        AnimalObservation AddItem(AnimalObservation item);
        void UpdateItem(AnimalObservation item);
        void DeleteByObservation(int observationId);
	}

}


