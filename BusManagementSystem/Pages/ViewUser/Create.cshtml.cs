using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entity;
using System.ComponentModel.DataAnnotations;
using SystemService.Interface;
using System.Text.Json;

namespace BusManagementSystem.Pages.ViewUser
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public CreateModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public string? Message { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a Role")]
        public int RoleId { get; set; }
        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Populate roles in the ViewBag to display in dropdown
            ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                // Re-populate roles in case of form submission error
                ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                return Page();
            }

            try
            {
                // Check if email already exists (instead of UserId)
                var existingAccount = _userService.GetAllAccount().FirstOrDefault(a => a.Email == User.Email);
                if (existingAccount != null)
                {
                    Message = "An account with this email already exists";
                    ModelState.AddModelError(string.Empty, Message);

                    // Re-populate roles in case of error
                    ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                    return Page();
                }

                // Assign RoleId based on the dropdown selection
                User.RoleId = RoleId;

                // Add the new user account
                _userService.AddAccount(User);

                Message = "Account created successfully";
                ModelState.AddModelError(string.Empty, Message);

                // Clear form fields after success if necessary
                ModelState.Clear();
                User = new User();

                // Redirect to the list page or remain on the form
                return RedirectToPage("/ViewUser/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ModelState.AddModelError(string.Empty, Message);

                // Re-populate roles in case of error
                ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
                return Page();
            }
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                if (account != null && account.RoleId == 1) // Assuming RoleId == 1 is Admin
                    return true;
            }
            return false;
        }
    }
}
