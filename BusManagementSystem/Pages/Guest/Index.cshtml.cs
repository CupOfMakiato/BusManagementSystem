using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;
using System.Linq;
using System.Collections.Generic;

namespace BusManagementSystem.Pages.Guest
{
    public class IndexModel : PageModel
    {
        private readonly IRouteService _routeService;

        public IndexModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public IList<BusinessObject.Entity.Route> Route { get; set; } = default!;
        public string? SearchQuery { get; set; }

        public IActionResult OnGet(string? searchQuery)
        {
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
    }
}
