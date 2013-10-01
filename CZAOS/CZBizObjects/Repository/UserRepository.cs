using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZDataObjects;

namespace CZBizObjects.Repository
{
    public class UserRepository 
    {
        public IEnumerable<CZUser> GetAll()
        {
            List<CZDataObjects.CZUser> users = UserList.GetActiveUsers();
            return users;            
        }

        public CZUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public CZUser Get(string username)
        {
            return UserList.GetUser(username);
        }

        public CZUser GetByEmail(string email)
        {
            return UserList.GetUserByEmail(email);
        }

        public CZUser Add(CZUser item)
        {
            return UserList.AddItem(item);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(CZUser item)
        {
            UserList.UpdateItem(item);
            return true;
        }
        
    }
}
