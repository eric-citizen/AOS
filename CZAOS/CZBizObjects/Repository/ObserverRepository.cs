using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ObserverRepository : IObservationRepository<Observer>
    {
        public IEnumerable<Observer> GetAll()
        {
            List<Observer> records = ObserverList.GetActive();
            return records;
        }

        public IEnumerable<Observer> GetAll(int observationId)
        {
            List<Observer> records = ObserverList.GetActive(observationId);
            return records;
        }

        public Observer Get(int id)
        {
            return ObserverList.Get(id);
        }

        public Observer Add(Observer item)
        {
            return ObserverList.AddItem(item);
        }

        public void Remove(int id)
        {
            ObserverList.DeleteItem(id);
        }

        public bool Update(Observer item)
        {
            ObserverList.UpdateItem(item);
            return true;
        }
    }
}
