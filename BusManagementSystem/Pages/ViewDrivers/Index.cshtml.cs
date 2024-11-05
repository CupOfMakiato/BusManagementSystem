using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewDrivers
{
    public class IndexModel : PageModel
    {
        private readonly IDriverService _driverService;

        public IndexModel(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public IList<Driver> Drivers { get; set; } = new List<Driver>();
        public string? SearchQuery { get; set; }
        public IActionResult OnGet(string? searchQuery)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            Drivers = _driverService.GetAllDrivers() ?? new List<Driver>();
            return Page();
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = System.Text.Json.JsonSerializer.Deserialize<User>(loginAccount);
                return account?.RoleId == 2;
            }
            return false;
        }
    }
}