﻿using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewUser
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public EditModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(int id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            var systemAccount = _userService.GetUserById(id);
            if (systemAccount == null)
            {
                Message = "User not found";
                return Page();
            }

            var roles = _roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName", systemAccount.RoleId);

            User = systemAccount;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
                return Page();

            try
            {
                var existingUser = _userService.GetUserById(User.UserId);
                if (existingUser == null)
                {
                    Message = "User not found";
                    return Page();
                }

                // Check if email is already in use by another user
                if (_userService.EmailExists(User.Email) && existingUser.Email != User.Email)
                {
                    ModelState.AddModelError("User.Email", "Email is already in use.");
                    return Page();
                }

                // Update user details
                existingUser.Name = User.Name;
                existingUser.DateOfBirth = User.DateOfBirth;
                existingUser.Email = User.Email;
                existingUser.PhoneNumber = User.PhoneNumber;
                if (!string.IsNullOrEmpty(User.Password))
                {
                    existingUser.Password = User.Password;
                }
                existingUser.RoleId = User.RoleId;
                existingUser.Status = User.Status;

                _userService.UpdateUser(existingUser);

                Message = "User updated successfully!";
                return RedirectToPage("/ViewUser/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
        }

        private bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = System.Text.Json.JsonSerializer.Deserialize<User>(loginAccount);
                return account?.RoleId == 1;
            }
            return false;
        }
    }
}
