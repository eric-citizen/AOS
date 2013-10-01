using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ObservationReportRepository : IObservationRepository<ObservationReport>
    {
        public IEnumerable<ObservationReport> GetAll()
        {
            List<ObservationReport> records = ObservationReportList.GetActive();
            return records;
        }

        public IEnumerable<ObservationReport> GetAll(int observationId)
        {
            List<ObservationReport> records = ObservationReportList.GetActive(observationId);
            return records;
        }

        public ObservationReport Get(int id)
        {
            return ObservationReportList.Get(id);
        }

        public ObservationReport Add(ObservationReport item)
        {
            return ObservationReportList.AddItem(item);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ObservationReport item)
        {
            ObservationReportList.UpdateItem(item);
            return true;
        }
    }
}
