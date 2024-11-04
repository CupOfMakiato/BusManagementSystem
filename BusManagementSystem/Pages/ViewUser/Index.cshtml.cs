using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewUser
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> User { get; set; } = new List<User>();

        public IActionResult OnGet()
        {
            // Session check
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Fetch all user accounts
            User = _userService.GetAllUsers() ?? new List<User>();

            return Page();
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                try
                {
                    var account = JsonSerializer.Deserialize<User>(loginAccount);
                    if (account != null && account.RoleId == 1) // Admin role check
                        return true;
                }
                catch (JsonException)
                {
                    // Handle potential deserialization issues
                    return false;
                }
            }
            return false;
        }
    }
}