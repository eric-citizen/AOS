using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class UserRegionRepository : ICZAOSRepository<UserRegion>
    {
        public IEnumerable<UserRegion> GetAll()
        {
            List<UserRegion> records = UserRegionList.GetActive();
            return records;
        }

        public UserRegion Get(int id)
        {
            return UserRegionList.Get(id);
        }

        public UserRegion Add(UserRegion item)
        {
            return UserRegionList.AddItem(item);
        }

        public void Remove(int id)
        {
            UserRegionList.DeleteItem(id);
        }

        public bool Update(UserRegion item)
        {
            UserRegionList.UpdateItem(item);
            return true;
        }
    }
}
