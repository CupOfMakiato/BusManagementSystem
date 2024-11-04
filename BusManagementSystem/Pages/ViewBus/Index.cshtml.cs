using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewBus
{
    public class IndexModel : PageModel
    {
        private readonly IBusService _busService;

        public IndexModel(IBusService busService)
        {
            _busService = busService;
        }

        public IList<Bus> Bus { get; set; } = default!;
        public string? SearchQuery { get; set; }

        public IActionResult OnGet(string? searchQuery)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Store the search query in the model for the Razor Page
            SearchQuery = searchQuery;

            // Fetch all news bus
            var buses = _busService.GetAllBuses();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                buses = buses.Where(b =>
                    (b.BusNumber.HasValue && b.BusNumber.ToString().Contains(searchQuery)) ||
                    (b.Driver != null && b.Driver.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (b.AssignedRoute != null && b.AssignedRoute.RouteName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Assign filtered list to the model property
            Bus = buses;
            return Page();
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                if (account != null && account.RoleId == 2)
                    return true;
            }
            return false;
        }
    }
}