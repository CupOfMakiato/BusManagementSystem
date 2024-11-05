using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewUser
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
                return NotFound();

            // Fetch the bus by ID
            User = _userService.GetUserById(id.Value);
            if (User == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
                return NotFound();

            // Find and delete the bus by ID
            try
            {
                var user = _userService.GetUserById(id.Value);
                if (user == null)
                    return NotFound();

                _userService.DeleteUser(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while deleting user.");
            }

            return RedirectToPage("/ViewUser/Index");
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                if (account != null && account.RoleId == 1)
                    return true;
            }
            return false;
        }
    }
}