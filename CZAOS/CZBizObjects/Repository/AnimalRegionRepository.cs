using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class AnimalRegionRepository  
    {
        public IEnumerable<AnimalRegion> GetAll()
        {
            List<AnimalRegion> records = AnimalRegionList.GetActive();
            return records;
        }

        public AnimalRegion Get(string animalRegionCode)
        {
            return AnimalRegionList.Get(animalRegionCode);
        }

        public AnimalRegion Add(AnimalRegion item)
        {
            return AnimalRegionList.AddItem(item);
        }

        public void Remove(string animalRegionCode)
        {
            AnimalRegionList.DeleteItem(animalRegionCode);
        }

        public bool Update(AnimalRegion item)
        {
            AnimalRegionList.UpdateItem(item);
            return true;
        }
    }
}
