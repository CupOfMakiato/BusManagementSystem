using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Guest
{
    public class IndexModel : PageModel
    {
        // Optional: Add any homepage-related properties if needed, like welcome messages or dynamic content.
        public string WelcomeMessage { get; set; } = "Welcome to the City Bus Management Center!";

        public string ContactInfo { get; set; } = "📞 Hotline: 19006836 | 📍 Address: 1 Kim Ma, Ba Dinh, Ha Noi";

        private readonly IRouteService _routeService;

        public IndexModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public IList<BusinessObject.Entity.Route> Route { get; set; } = default!;
        public string? SearchQuery { get; set; }

        public IActionResult OnGet(string? searchQuery)
        {
            // Session check - Optional for guest
            if (!CheckGuestSession())
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
            Route = routes;

            return Page();
        }

        public bool CheckGuestSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                try
                {
                    var account = JsonSerializer.Deserialize<User>(loginAccount);
                    // No specific role check for guests
                    if (account != null)
                        return true;
                }
                catch (JsonException)
                {
                    // Handle potential deserialization issues
                    return false;
                }
            }
            return true; // Allow access even if session is not present for guest
        }
    }
}