using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class TimedInfoRepository : ICZAOSRepository<TimedInfo>
    {
        public IEnumerable<TimedInfo> GetAll()
        {
            List<TimedInfo> records = TimedInfoList.GetActive();
            return records;
        }

        public TimedInfo Get(int id)
        {
            return TimedInfoList.Get(id);
        }

        public TimedInfo Add(TimedInfo item)
        {
            return TimedInfoList.AddItem(item);
        }

        public void Remove(int id)
        {
            TimedInfoList.DeleteItem(id);
        }

        public bool Update(TimedInfo item)
        {
            TimedInfoList.UpdateItem(item);
            return true;
        }
    }
}
