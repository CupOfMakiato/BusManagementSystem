using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;

        public UserRepository(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public IEnumerable<User> GetAllUsers() => _userDAO.GetAllUsers();

        public User GetUserById(int id) => _userDAO.GetUserById(id);

        public void AddUser(User user) => _userDAO.AddUser(user);

        public void UpdateUser(User user) => _userDAO.UpdateUser(user);

        public void DeleteUser(int id) => _userDAO.DeleteUser(id);
    }
}
