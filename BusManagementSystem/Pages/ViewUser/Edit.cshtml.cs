using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewUser
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;  // Assuming a service to fetch roles

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

            if (id == null)
            {
                Message = "Not Found";
                return Page();
            }

            var systemAccount = _userService.GetUserById(id);
            if (systemAccount == null)
            {
                Message = "Not Found";
                return Page();
            }

            // Load available roles and pass them to the page using ViewBag
            var roles = _roleService.GetAllRoles(); // Fetch roles
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName", systemAccount.RoleId);

            User = systemAccount;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var account = _userService.GetUserById(User.UserId);
                if (account == null)
                {
                    Message = "Not Found";
                    return Page();
                }

                // Update all the necessary fields from the form, including Status
                account.Name = User.Name;
                account.DateOfBirth = User.DateOfBirth;
                account.Email = User.Email;
                account.PhoneNumber = User.PhoneNumber;
                if (!string.IsNullOrEmpty(User.Password))
                {
                    // Only update password if a new one is entered
                    account.Password = User.Password;
                }
                account.RoleId = User.RoleId;  // Update Role
                account.Status = User.Status;  // Update Status here

                // Save changes
                _userService.UpdateUser(account);

                Message = "Update successful!";
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