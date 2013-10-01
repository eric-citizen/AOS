using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class AnimalGroupRepository : IObservationRepository<AnimalGroup>
    {
        public IEnumerable<AnimalGroup> GetAll()
        {
            List<AnimalGroup> records = AnimalGroupList.GetActive();
            return records;
        }

        public IEnumerable<AnimalGroup> GetAll(int observationId)
        {
            List<AnimalGroup> records = AnimalGroupList.GetActiveByObservationID(observationId);
            return records;
        }

        public AnimalGroup Get(int id)
        {
            return AnimalGroupList.Get(id);
        }

        public AnimalGroup Add(AnimalGroup item)
        {
            return AnimalGroupList.AddItem(item);
        }

        public void Remove(int observationId)
        {
            AnimalGroupList.DeleteByObservation(observationId);
        }

        public bool Update(AnimalGroup item)
        {
            AnimalGroupList.UpdateItem(item);
            return true;
        }
    }
}
