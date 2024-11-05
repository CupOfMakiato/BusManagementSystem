using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user) => UserDAO.GetInstance().AddUser(user);

        public User CheckLogin(string username, string password) => UserDAO.GetInstance().Checklogin(username, password);

        public void DeleteUser(User user) => UserDAO.GetInstance().DeleteUser(user);

        public bool EmailExists(string email)
        {
            return UserDAO.GetInstance().EmailExists(email);
        }

        public List<User> GetAllUsers() => UserDAO.GetInstance().GetAllUser();

        public User GetUserById(int userId) => UserDAO.GetInstance().GetUserById(userId);

        public void SoftDeleteUser(User u)
        {
            UserDAO.GetInstance().SoftDeleteUser(u);
        }

        public void UpdateUser(User user) => UserDAO.GetInstance().UpdateUser(user);

        public bool UserIdExists(int userId)
        {
            return UserDAO.GetInstance().UserIdExists(userId);
        }
    }
}