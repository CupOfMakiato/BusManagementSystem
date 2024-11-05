using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewRoute
{
    public class IndexModel : PageModel
    {
        private readonly IRouteService _routeService; // Assuming a service layer

        public IndexModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public IList<BusinessObject.Entity.Route> Routes { get; set; } = new List<BusinessObject.Entity.Route>();
        public string? SearchQuery { get; set; }

        public IActionResult OnGet(string? searchQuery)
        {
            // Session check - Optional for guest
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Store the search query in the model for the Razor Page
            SearchQuery = searchQuery;

            // Fetch all routes
            var routes = _routeService.GetAllRoutes();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                routes = routes.Where(r =>
                    (r.RouteName != null && r.RouteName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (r.StartLocation != null && r.StartLocation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (r.EndLocation != null && r.EndLocation.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Assign filtered list to the model property
            Routes = routes;

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
                    if (account != null && account.RoleId == 2) // Staff role check
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