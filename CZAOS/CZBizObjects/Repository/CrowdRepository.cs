using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class CrowdRepository : ICZAOSRepository<Crowd>
    {
        public IEnumerable<Crowd> GetAll()
        {
            List<Crowd> records = CrowdList.GetActive();
            return records;
        }

        public Crowd Get(int id)
        {
            return CrowdList.Get(id);
        }

        public Crowd Add(Crowd item)
        {
            return CrowdList.AddItem(item);
        }

        public void Remove(int id)
        {
            CrowdList.DeleteItem(id);
        }

        public bool Update(Crowd item)
        {
            CrowdList.UpdateItem(item);
            return true;
        }
    }
}
