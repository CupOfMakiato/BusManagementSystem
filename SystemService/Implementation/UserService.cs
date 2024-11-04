using BusinessObject.Entity;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user) => _userRepository.AddUser(user);

        public void UpdateUser(User user) => _userRepository.UpdateUser(user);

        public void DeleteUser(User user) => _userRepository.DeleteUser(user);

        public List<User> GetAllUsers() => _userRepository.GetAllUsers();

        public User CheckLogin(string username, string password) => _userRepository.CheckLogin(username, password);

        public User GetUserById(int userId) => _userRepository.GetUserById(userId);
    }
}