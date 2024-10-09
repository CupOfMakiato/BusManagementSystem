using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDAO
{
    public class UserDAO
    {
        private readonly BusManagementSystemContext _systemContext;
        public UserDAO(BusManagementSystemContext systemContext)
        {
            _systemContext = systemContext;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _systemContext.Users.ToList();
        }
        public User GetUserById(int id)
        {
            return _systemContext.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _systemContext.Users.Add(user);
            _systemContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _systemContext.Users.Update(user);
            _systemContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _systemContext.Users.Remove(user);
                _systemContext.SaveChanges();
            }
        }
    }
}
