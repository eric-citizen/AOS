using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class WeatherRepository : IObservationRepository<Weather>
    {
        public IEnumerable<Weather> GetAll()
        {
            List<Weather> records = WeatherList.GetActive();
            return records;
        }

        public IEnumerable<Weather> GetAll(int observationId)
        {
            List<Weather> records = WeatherList.GetActive(observationId);
            return records;
        }

        public Weather Get(int id)
        {
            return WeatherList.Get(id);
        }

        public Weather Add(Weather item)
        {
            return WeatherList.AddItem(item);
        }

        public void Remove(int id)
        {
            WeatherList.DeleteItem(id);
        }

        public bool Update(Weather item)
        {
            WeatherList.UpdateItem(item);
            return true;
        }
    }
}
