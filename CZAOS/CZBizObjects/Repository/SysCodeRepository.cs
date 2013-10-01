using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class SysCodeRepository //: ICZAOSRepository<SysCode>
    {
        public IEnumerable<SysCode> GetAll()
        {
            List<SysCode> records = SysCodeList.GetAll();
            return records;
        }

        public SysCode Get(int id)
        {
            return SysCodeList.Get(id);
        }

        public SysCode Get(string key)
        {
            return SysCodeList.Get(key);
        }
        
    }
}
