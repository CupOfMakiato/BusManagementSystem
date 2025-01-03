﻿using BusinessObject.Entity;

namespace SystemRepository.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        User GetUserById(int userId);

        List<User> GetAllUsers();

        User CheckLogin(string username, string password);
        bool UserIdExists(int userId);
        bool EmailExists(string email);
        void SoftDeleteUser(User u);
        bool UserHasAssociations(int userId);
    }
}