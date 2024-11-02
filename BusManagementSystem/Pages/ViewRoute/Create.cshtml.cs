using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Entity;
using System.ComponentModel.DataAnnotations;
using SystemService.Interface;
using System.Text.Json;

namespace BusManagementSystem.Pages.ViewRoute
{
    public class CreateModel : PageModel
    {
        private readonly IRouteService _routeService;

        public CreateModel(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [BindProperty]
        public BusinessObject.Entity.Route Route { get; set; } = new BusinessObject.Entity.Route();

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

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
                // Check if route already exists by name
                var existingRoute = _routeService.GetAllRoutes()
                    .FirstOrDefault(r => r.RouteName == Route.RouteName);

                if (existingRoute != null)
                {
                    Message = "A route with this name already exists";
                    ModelState.AddModelError(string.Empty, Message);
                    return Page();
                }

                // Add the new route
                _routeService.AddRoute(Route);

                Message = "Route created successfully";
                ModelState.AddModelError(string.Empty, Message);

                // Clear form fields after success if necessary
                ModelState.Clear();
                Route = new BusinessObject.Entity.Route();

                // Redirect to the list page or remain on the form
                return RedirectToPage("/ViewRoute/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                if (account != null && account.RoleId == 2) // Assuming RoleId == 1 is Admin
                    return true;
            }
            return false;
        }
    }
}
