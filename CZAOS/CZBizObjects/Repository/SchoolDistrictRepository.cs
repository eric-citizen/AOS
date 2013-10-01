using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class SchoolDistrictRepository : ICZAOSRepository<SchoolDistrict>
    {
        public IEnumerable<SchoolDistrict> GetAll()
        {
            List<SchoolDistrict> records = SchoolDistrictList.GetActive();
            return records;
        }

        public SchoolDistrict Get(int id)
        {
            return SchoolDistrictList.Get(id);
        }

        public SchoolDistrict Add(SchoolDistrict item)
        {
            return SchoolDistrictList.AddItem(item);
        }

        public void Remove(int id)
        {
            SchoolDistrictList.DeleteItem(id);
        }

        public bool Update(SchoolDistrict item)
        {
            SchoolDistrictList.UpdateItem(item);
            return true;
        }
    }
}
