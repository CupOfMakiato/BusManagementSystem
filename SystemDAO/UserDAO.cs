using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;

namespace SystemDAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;

        public UserDAO()
        {
        }

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAO();
                }
                return instance;
            }
        }

        public List<User> GetAllUser()
        {
            var list = new List<User>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Users
                    .Include(a => a.Role).ToList();
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }

        public User GetUserById(int userId)
        {
            using var db = new BusManagementSystemContext();
            return db.Users.FirstOrDefault(x => x.UserId.Equals(userId));
        }

        public User Checklogin(string email, string password)
        {
            var u = new User();
            try
            {
                using (var db = new BusManagementSystemContext())
                {
                    u = db.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                    return u;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(User u)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                db.Entry<User>(u).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(User u)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                var p1 = db.Users.SingleOrDefault(x => x.UserId == u.UserId);
                db.Users.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddUser(User u)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                db.Users.Add(u);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}