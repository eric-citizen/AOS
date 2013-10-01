using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    //public interface ISchoolRepository
    //{
    //    IEnumerable<School> GetAll();
    //    School Get(int id);
    //    School Add(School item);
    //    void Remove(int id);
    //    bool Update(School item);
    //}

    public interface ICZAOSRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T item);
        void Remove(int id);
        bool Update(T item);
    }

    public interface IObservationRepository<T> : ICZAOSRepository<T>
    {
        IEnumerable<T> GetAll(int obsId);        
    }
}
