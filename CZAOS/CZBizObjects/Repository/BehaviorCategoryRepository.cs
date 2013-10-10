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
        static readonly ICZAOSRepository<ExhibitBehavior> ebRepository = new ExhibitBehaviorRepository();

        public IEnumerable<BehaviorCategory> GetAll()
        {
            List<BehaviorCategory> records = BehaviorCategoryList.GetActive();
            return records;
        }

        public IEnumerable<BehaviorCategory> GetAll(int exhibitId)
        {
            IEnumerable<BehaviorCategory> records = BehaviorCategoryList.GetActive();
            var exhibitBehaviors = ebRepository.GetAll().Where(eb => eb.ExhibitID == exhibitId);
            records = records.Where(c => exhibitBehaviors.Any(e => e.BvrCatID == c.BvrCatID));
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
