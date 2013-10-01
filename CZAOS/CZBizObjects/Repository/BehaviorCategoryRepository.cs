using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class BehaviorCategoryRepository : ICZAOSRepository<BehaviorCategory>
    {
        public IEnumerable<BehaviorCategory> GetAll()
        {
            List<BehaviorCategory> records = BehaviorCategoryList.GetActive();
            return records;
        }

        public BehaviorCategory Get(int id)
        {
            return BehaviorCategoryList.Get(id);
        }

        public BehaviorCategory Add(BehaviorCategory item)
        {
            return BehaviorCategoryList.AddItem(item);
        }

        public void Remove(int id)
        {
            BehaviorCategoryList.DeleteItem(id);
        }

        public bool Update(BehaviorCategory item)
        {
            BehaviorCategoryList.UpdateItem(item);
            return true;
        }
    }
}
