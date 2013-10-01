using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class BehaviorRepository : ICZAOSRepository<Behavior>
    {
        public IEnumerable<Behavior> GetAll()
        {
            List<Behavior> records = BehaviorList.GetActive();
            return records;
        }

        public Behavior Get(int id)
        {
            return BehaviorList.Get(id);
        }

        public Behavior Add(Behavior item)
        {
            return BehaviorList.AddItem(item);
        }

        public void Remove(int id)
        {
            BehaviorList.DeleteItem(id);
        }

        public bool Update(Behavior item)
        {
            BehaviorList.UpdateItem(item);
            return true;
        }
    }
}
