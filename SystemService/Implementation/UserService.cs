using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddAccount(User user)
        {
            _userRepository.AddAccount(user);
        }

        public void DeleteAccount(User user)
        {
            _userRepository.DeleteAccount(user);
        }

        public User? GetAccountByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetAccountByEmailAndPassword(email, password);
        }

        public User GetAccountById(short userId)
        {
            return _userRepository.GetAccountById(userId);
        }

        public List<User> GetAllAccount()
        {
            return _userRepository.GetAllAccount();
        }

        public void UpdateAccount(User user)
        {
            _userRepository.UpdateAccount(user);
        }

        public User VerifyAccount(User user)
        {
            return _userRepository.VerifyAccount(user);
        }
    }
}
