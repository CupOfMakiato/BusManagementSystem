using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user) => UserDAO.Instance.AddUser(user);

        public User CheckLogin(string username, string password) => UserDAO.Instance.Checklogin(username, password);

        public void DeleteUser(User user) => UserDAO.Instance.DeleteUser(user);

        public List<User> GetAllUsers() => UserDAO.Instance.GetAllUser();

        public User GetUserById(int userId) => UserDAO.Instance.GetUserById(userId);

        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
    }
}