using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entity;
using SystemDAO;
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

        public IActionResult OnGet()
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
                return account?.RoleId == 1;
            }
            return false;
        }
    }
}
