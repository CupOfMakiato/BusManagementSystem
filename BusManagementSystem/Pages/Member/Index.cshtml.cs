﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

=======
using System.Text.Json;
using SystemService.Interface;


namespace BusManagementSystem.Pages.Member
{
    public class IndexModel : PageModel
    {

        // Optional: Add any homepage-related properties if needed, like welcome messages or dynamic content.
        public string WelcomeMessage { get; set; } = "Welcome to the City Bus Management Center!";
        public string ContactInfo { get; set; } = "📞 Hotline: 19006836 | 📍 Address: 1 Kim Ma, Ba Dinh, Ha Noi";

        public IActionResult OnGet()
        {
            // This method only serves to load the homepage, so we don't need to fetch or filter any route data here.

        private readonly IRouteService _routeService;

        public IndexModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public IList<BusinessObject.Entity.Route> Route { get; set; } = default!;
        public string? SearchQuery { get; set; }

        public IActionResult OnGet(string? searchQuery)
        {
            // Session check
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
            Route = routes;

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
                    if (account != null && account.RoleId == 3) // Member role check
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

