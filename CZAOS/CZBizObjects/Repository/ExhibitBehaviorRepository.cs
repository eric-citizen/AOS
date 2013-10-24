using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ExhibitBehaviorRepository : ICZAOSRepository<ExhibitBehavior>
    {
        public IEnumerable<ExhibitBehavior> GetAll()
        {
            List<ExhibitBehavior> records = ExhibitBehaviorList.GetActive();
            return records;
        }

        public IEnumerable<ExhibitBehavior> GetAll(int id)
        {
            List<ExhibitBehavior> records = ExhibitBehaviorList.GetActive().Where(x => x.ExhibitID == id).ToList();
            return records;
        }

        public ExhibitBehavior Get(int id)
        {
            return ExhibitBehaviorList.Get(id);
        }

        public ExhibitBehavior Add(ExhibitBehavior item)
        {
            return ExhibitBehaviorList.AddItem(item);
        }

        public void Remove(int id)
        {
            ExhibitBehaviorList.DeleteItem(id);
        }

        public bool Update(ExhibitBehavior item)
        {
            ExhibitBehaviorList.UpdateItem(item);
            return true;
        }
    }
}
