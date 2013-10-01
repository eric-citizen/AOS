using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class GradeRepository : ICZAOSRepository<Grade>
    {
        public IEnumerable<Grade> GetAll()
        {
            List<Grade> records = GradeList.GetActive();
            return records;
        }

        public Grade Get(int id)
        {
            return GradeList.Get(id);
        }

        public Grade Add(Grade item)
        {
            return GradeList.AddItem(item);
        }

        public void Remove(int id)
        {
            GradeList.DeleteItem(id);
        }

        public bool Update(Grade item)
        {
            GradeList.UpdateItem(item);
            return true;
        }
    }
}
