using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class SchoolRepository : ICZAOSRepository<School> //ISchoolRepository 
    {       

        public IEnumerable<School> GetAll()
        {
            List<CZDataObjects.School> schools = SchoolList.GetActiveSchools();
            return schools;            
        }

        public School Get(int id)
        {
            return SchoolList.GetSchool(id);
        }

        public School Add(School item)
        {
            return SchoolList.AddItem(item);
        }

        public void Remove(int id)
        {
            SchoolList.DeleteItem(id);
        }

        public bool Update(School item)
        {
            SchoolList.UpdateItem(item);
            return true;
        }
        
    }
}
