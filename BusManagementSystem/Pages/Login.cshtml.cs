using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;
using BusManagementSystem.Models;
using System.Text.Json;

namespace BusManagementSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; } // Property to hold the error message

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {
            // Optional: logic for handling initial page load
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAccountByEmailAndPassword(Input.Email, Input.Password);
                if (user != null)
                {
                    // Store user data in session
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("LoginSession", JsonSerializer.Serialize(user));

                    // Redirect based on role
                    switch (user.RoleId)
                    {
                        case 1: // Admin role
                            return RedirectToPage("/ViewUser/Index");
                        case 2: // Staff role
                            return RedirectToPage("/ViewRoute/Index");
                        case 3: // Member role
                            return RedirectToPage("/Member/Index");
                    }
                }
                else
                {
                    // Set error message if login fails
                    ErrorMessage = "Invalid email or password. Please try again.";
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                }
            }
            return Page();
        }
    }
}