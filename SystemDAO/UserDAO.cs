using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        public UserDAO()
        {

        }
        public static UserDAO getInstance()
        {
            if (instance == null)
            {
                instance = new UserDAO();
            }
            return instance;
        }
        public User VerifyAccount(User acc)
        {
            User mb;
            try
            {
                var db = new BusManagementSystemContext();
                mb = db.Users.FirstOrDefault(m => m.Email == acc.Email && m.Password == acc.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return mb;
        }
        public List<User> GetAllUser()
        {
            var list = new List<User>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Users.ToList();
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }

        public User GetAccountById(short userId)
        {
            using var db = new BusManagementSystemContext();
            return db.Users.FirstOrDefault(x => x.UserId.Equals(userId));
        }

        public User? GetAccountByEmailAndPassword(string email, string password)
        {
            using var db = new BusManagementSystemContext();
            return db.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public void UpdateAccount(User u)
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

        public void DeleteAccount(User u)
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

        public void AddAccount(User u)
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
