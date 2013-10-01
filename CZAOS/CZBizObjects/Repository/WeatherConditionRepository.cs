using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class WeatherConditionRepository : ICZAOSRepository<WeatherCondition>
    {
        public IEnumerable<WeatherCondition> GetAll()
        {
            List<WeatherCondition> records = WeatherConditionList.GetActive();
            return records;
        }

        public WeatherCondition Get(int id)
        {
            return WeatherConditionList.Get(id);
        }

        public WeatherCondition Add(WeatherCondition item)
        {
            return WeatherConditionList.AddItem(item);
        }

        public void Remove(int id)
        {
            WeatherConditionList.DeleteItem(id);
        }

        public bool Update(WeatherCondition item)
        {
            WeatherConditionList.UpdateItem(item);
            return true;
        }
    }
}
