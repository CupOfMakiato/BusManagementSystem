using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;
using BusinessObject.Entity;

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

        public IActionResult OnGet()
        {
            // Session check
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Fetch all routes
            Routes = _routeService.GetAllRoutes() != null ? _routeService.GetAllRoutes() : new List<BusinessObject.Entity.Route>();

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
