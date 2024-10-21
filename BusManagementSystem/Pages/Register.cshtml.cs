using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SystemService.Interface;

namespace BusManagementSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;  // Assuming a role service exists

        public RegisterModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        public IList<Role> Roles { get; set; } = default!;

        public void OnGet()
        {
            // Load roles to display in the dropdown
            Roles = _roleService.GetAllRoles();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Return the page with validation errors
                Roles = _roleService.GetAllRoles();
                return Page();
            }

            // Check if the email already exists
            var existingUser = _userService.GetAllAccount().FirstOrDefault(u => u.Email.Equals(Input.Email, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                // Add a model state error for the email field
                ModelState.AddModelError("Input.Email", "This email existed!");

                // Return the page with validation errors
                Roles = _roleService.GetAllRoles();
                return Page();
            }

            // Create a new user
            var newUser = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                DateOfBirth = Input.DateOfBirth,
                PhoneNumber = Input.PhoneNumber,
                Password = Input.Password,  // Consider hashing the password
                RoleId = 3,
                Status = 1  // Active by default
            };

            _userService.AddAccount(newUser);

            // Redirect to the login page or a success page
            return RedirectToPage("/Login");
        }
    }

    public class RegisterInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}