using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class AnimalRepository : ICZAOSRepository<Animal>
    {
        public IEnumerable<Animal> GetAll()
        {
            List<Animal> records = AnimalList.GetActiveAnimals();
            return records;
        }

        public Animal Get(int id)
        {
            return AnimalList.GetAnimal(id);
        }

        public Animal Add(Animal item)
        {
            return AnimalList.AddItem(item);
        }

        public void Remove(int id)
        {
            AnimalList.DeleteItem(id);
        }

        public bool Update(Animal item)
        {
            AnimalList.UpdateItem(item);
            return true;
        }
    }
}
