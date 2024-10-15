using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService.Interface
{
    public interface IUserService
    {
        void AddAccount(User user);
        void UpdateAccount(User user);
        void DeleteAccount(User user);
        User GetAccountById(short userId);
        User? GetAccountByEmailAndPassword(string email, string password);
        List<User> GetAllAccount();
        User VerifyAccount(User user);
    }
}
