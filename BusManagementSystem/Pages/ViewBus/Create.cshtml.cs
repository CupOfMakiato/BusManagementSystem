using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewBus
{
    public class CreateModel : PageModel
    {
        private readonly IBusService _busService;
        private readonly IRouteService _routeService;
        private readonly IDriverService _driverService;

        public CreateModel(IBusService busService, IRouteService routeService, IDriverService driverService)
        {
            _busService = busService;
            _routeService = routeService;
            _driverService = driverService;
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Populate dropdowns for route and driver selection
            ViewData["AssignedRouteId"] = new SelectList(_routeService.GetAllRoutes(), "RouteId", "RouteName");
            ViewData["DriverId"] = new SelectList(_driverService.GetAllDrivers().Where(d => d.Status == 1), "DriverId", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
                return Page();

            if (Bus.BusNumber <= 0)
            {
                ModelState.AddModelError("Bus.BusNumber", "Bus number must be greater than zero.");
                return Page();
            }

            // Check if the BusNumber is unique
            bool busNumberExists = _busService.CheckBusNumberExists((int)Bus.BusNumber);
            if (busNumberExists)
            {
                ModelState.AddModelError("Bus.BusNumber", "The Bus Number must be unique.");
                return Page();
            }

            // Set creation date and save the new bus
            Bus.CreatedAt = DateTime.UtcNow;

            try
            {
                _busService.AddBus(Bus);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while creating the bus. Please try again.");
                return Page();
            }

            return RedirectToPage("/ViewBus/Index");
        }

        private bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                var account = JsonSerializer.Deserialize<User>(loginAccount);
                return account != null && account.RoleId == 2;
            }
            return false;
        }
    }
}
