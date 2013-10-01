using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ObservationRecordRepository : IObservationRepository<ObservationRecord>
    {
        public IEnumerable<ObservationRecord> GetAll()
        {
            List<ObservationRecord> records = ObservationRecordList.GetActive();
            return records;
        }

        public IEnumerable<ObservationRecord> GetAll(int observationId)
        {
            List<ObservationRecord> records = ObservationRecordList.GetActive(observationId);
            return records;
        }

        public ObservationRecord Get(int id)
        {
            return ObservationRecordList.Get(id);
        }

        public ObservationRecord Add(ObservationRecord item)
        {
            return ObservationRecordList.AddItem(item);
        }

        public void Remove(int observationId)
        {
            ObservationRecordList.DeleteByObservation(observationId);
        }

        public bool Update(ObservationRecord item)
        {
            ObservationRecordList.UpdateItem(item);
            return true;
        }
    }
}
