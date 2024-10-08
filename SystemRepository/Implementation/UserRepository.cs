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
        public void AddAccount(User user)
        {
            UserDAO.getInstance().AddAccount(user);
        }

        public void DeleteAccount(User user)
        {
            UserDAO.getInstance().DeleteAccount(user);
        }

        public User? GetAccountByEmailAndPassword(string email, string password)
        {
            return UserDAO.getInstance().GetAccountByEmailAndPassword(email, password);
        }

        public User GetAccountById(short userId)
        {
            return UserDAO.getInstance().GetAccountById(userId);
        }

        public List<User> GetAllAccount()
        {
            return UserDAO.getInstance().GetAllUser();
        }

        public void UpdateAccount(User user)
        {
            UserDAO.getInstance().UpdateAccount(user);
        }

        public User VerifyAccount(User user)
        {
            return UserDAO.getInstance().VerifyAccount(user);
        }
    }
}
