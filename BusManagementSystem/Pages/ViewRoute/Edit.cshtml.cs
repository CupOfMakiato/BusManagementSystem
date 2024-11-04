using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewRoute
{
    public class EditModel : PageModel
    {
        private readonly IRouteService _routeService;

        public EditModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [BindProperty]
        public BusinessObject.Entity.Route Route { get; set; } = default!;

        public string? Message { get; set; }

        public IActionResult OnGet(short? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
            {
                Message = "Not Found";
                return Page();
            }

            var route = _routeService.GetAllRoutes().FirstOrDefault(m => m.RouteId == id);
            if (route == null)
            {
                Message = "Not Found";
                return Page();
            }

            Route = route;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var route = _routeService.GetAllRoutes().FirstOrDefault(r => r.RouteId == Route.RouteId);
                if (route == null)
                {
                    Message = "Not Found";
                    return Page();
                }

                // Update all the necessary fields from the form
                route.RouteName = Route.RouteName;
                route.StartLocation = Route.StartLocation;
                route.EndLocation = Route.EndLocation;
                route.Distance = Route.Distance;
                route.Duration = Route.Duration;

                // Save changes
                _routeService.UpdateRoute(route);

                Message = "Update successful!";
                return RedirectToPage("/ViewRoute/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
        }

        private bool CheckSession()
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