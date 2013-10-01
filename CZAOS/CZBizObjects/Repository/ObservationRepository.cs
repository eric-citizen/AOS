using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ObservationRepository : ICZAOSRepository<Observation>
    {
        public IEnumerable<Observation> GetAll()
        {
            List<Observation> records = ObservationList.GetActive();
            return records;
        }

        public Observation Get(int id)
        {
            return ObservationList.Get(id);
        }

        public Observation Add(Observation item)
        {
            return ObservationList.AddItem(item);
        }

        public void Remove(int id)
        {
            ObservationList.DeleteItem(id);
        }

        public bool Update(Observation item)
        {
            ObservationList.UpdateItem(item);
            return true;
        }
    }
}
