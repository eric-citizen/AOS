using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class WindRepository : ICZAOSRepository<Wind>
    {
        public IEnumerable<Wind> GetAll()
        {
            List<Wind> records = WindList.GetActive();
            return records;
        }

        public Wind Get(int id)
        {
            return WindList.Get(id);
        }

        public Wind Add(Wind item)
        {
            return WindList.AddItem(item);
        }

        public void Remove(int id)
        {
            WindList.DeleteItem(id);
        }

        public bool Update(Wind item)
        {
            WindList.UpdateItem(item);
            return true;
        }
    }
}
