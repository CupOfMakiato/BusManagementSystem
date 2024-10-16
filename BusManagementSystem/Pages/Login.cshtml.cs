using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;
using BusManagementSystem.Models;

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

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {
            // Có thể thêm logic nếu cần
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAccountByEmailAndPassword(Input.Email, Input.Password);
                if (user != null)
                {
                    
                    HttpContext.Session.SetString("UserEmail", user.Email);

                  
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Page();
        }
    }
}
