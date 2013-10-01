using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class LocationRepository : ICZAOSRepository<Location>
    {
        public IEnumerable<Location> GetAll()
        {
            List<Location> records = LocationList.GetActive();
            return records;
        }

        public Location Get(int id)
        {
            return LocationList.Get(id);
        }

        public Location Add(Location item)
        {
            return LocationList.AddItem(item);
        }

        public void Remove(int id)
        {
            LocationList.DeleteItem(id);
        }

        public bool Update(Location item)
        {
            LocationList.UpdateItem(item);
            return true;
        }
    }
}
