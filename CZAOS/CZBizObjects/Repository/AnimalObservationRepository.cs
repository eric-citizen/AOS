using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class AnimalObservationRepository : IObservationRepository<AnimalObservation>
    {
        public IEnumerable<AnimalObservation> GetAll()
        {
            List<AnimalObservation> records = AnimalObservationList.GetActive();
            return records;
        }

        public IEnumerable<AnimalObservation> GetAll(int observationId)
        {
            List<AnimalObservation> records = AnimalObservationList.GetActive(observationId);
            return records;
        }        

        public AnimalObservation Get(int id)
        {
            //stub
            return null;
        }

        public AnimalObservation Add(AnimalObservation item)
        {
            return AnimalObservationList.AddItem(item);
        }

        public void Remove(int id)
        {
            //stub
        }

        public bool Update(AnimalObservation item)
        {
            AnimalObservationList.UpdateItem(item);
            return true;
        }
    }
}
