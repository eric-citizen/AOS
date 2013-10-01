using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class ExhibitRepository : ICZAOSRepository<Exhibit>
    {
        public IEnumerable<Exhibit> GetAll()
        {
            List<Exhibit> records = ExhibitList.GetActive();
            return records;
        }

        public Exhibit Get(int id)
        {
            return ExhibitList.Get(id);
        }

        public Exhibit Add(Exhibit item)
        {
            return ExhibitList.AddItem(item);
        }

        public void Remove(int id)
        {
            ExhibitList.DeleteItem(id);
        }

        public bool Update(Exhibit item)
        {
            ExhibitList.UpdateItem(item);
            return true;
        }
    }
}
