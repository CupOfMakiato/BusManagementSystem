using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
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
        public CreateUserInputModel Input { get; set; } = new CreateUserInputModel();
        [BindProperty]
        public User User { get; set; } = new User();

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            // Restrict roles to "Staff Driver" and "Member"
            var roles = _roleService.GetAllRoles()
                .Where(role => role.RoleName == "Staff" || role.RoleName == "Driver" || role.RoleName == "Member")
                .ToList();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            var roles = _roleService.GetAllRoles()
                    .Where(role => role.RoleName == "Staff" || role.RoleName == "Driver" || role.RoleName == "Member")
                    .ToList();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            //if (!ModelState.IsValid)

            //    return Page();

            try
            {
                // Check if Email already exists
                if (_userService.EmailExists(Input.Email))
                {
                    ModelState.AddModelError("Input.Email", "Email is already in use.");
                    return Page();
                }

                // Check if UserId already exists (if specified)
                if (Input.UserId != 0 && _userService.UserIdExists(Input.UserId))
                {
                    ModelState.AddModelError("Input.UserId", "User ID already exists.");
                    return Page();
                }

                // Create and save new user
                var newUser = new User
                {
                    UserId = Input.UserId,
                    Name = Input.Name,
                    Email = Input.Email,
                    DateOfBirth = Input.DateOfBirth,
                    PhoneNumber = Input.PhoneNumber,
                    Password = Input.Password,
                    RoleId = Input.RoleId,
                    Status = 1  // Active by default
                };

                _userService.AddUser(newUser);
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

        public class CreateUserInputModel
        {
            [Required]
            public int UserId { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address format.")]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [PastDate(ErrorMessage = "Date of birth must be a past date.")]
            public DateTime? DateOfBirth { get; set; }

            [Phone]
            public string? PhoneNumber { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Password must be at least 6 characters long, contain at least one uppercase letter, one number, and one special character.")]
            public string Password { get; set; } = string.Empty;

            [Required]
            public int RoleId { get; set; }
        }

        public class PastDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateValue)
                {
                    if (dateValue < DateTime.Today)
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
                }
                return new ValidationResult("Invalid date format.");
            }
        }
    }

}
