using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ExhibitLocationRepository : ICZAOSRepository<ExhibitLocation>
    {
        public IEnumerable<ExhibitLocation> GetAll()
        {
            List<ExhibitLocation> records = ExhibitLocationList.GetActive();
            return records;
        }

        public ExhibitLocation Get(int id)
        {
            return ExhibitLocationList.Get(id);
        }

        public ExhibitLocation Add(ExhibitLocation item)
        {
            return ExhibitLocationList.AddItem(item);
        }

        public void Remove(int id)
        {
            ExhibitLocationList.DeleteItem(id);
        }

        public bool Update(ExhibitLocation item)
        {
            ExhibitLocationList.UpdateItem(item);
            return true;
        }
    }
}
