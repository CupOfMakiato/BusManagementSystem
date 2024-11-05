using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemService.Interface;

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

        public IActionResult OnGet()
        {
            var roles = _roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
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
                // Check if Email already exists
                if (_userService.EmailExists(User.Email))
                {
                    ModelState.AddModelError("User.Email", "Email is already in use.");
                    return Page();
                }

                // Check if UserId already exists (if specified)
                if (User.UserId != 0 && _userService.UserIdExists(User.UserId))
                {
                    ModelState.AddModelError("User.UserId", "User ID already exists.");
                    return Page();
                }

                // Set creation date and save new user
                User.DateOfBirth = DateTime.UtcNow;
                _userService.AddUser(User);

                Message = "User created successfully!";
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
