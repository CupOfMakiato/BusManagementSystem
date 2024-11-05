using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewBus
{
    public class EditModel : PageModel
    {
        private readonly IBusService _busService;
        private readonly IDriverService _driverService;
        private readonly IRouteService _routeService;

        public EditModel(IBusService busService, IRouteService routeService, IDriverService driverService)
        {
            _busService = busService;
            _routeService = routeService;
            _driverService = driverService;
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (id == null)
                return NotFound();

            // Fetch the bus by ID
            Bus = _busService.GetBusById(id.Value);
            if (Bus == null)
                return NotFound();

            // Load route and driver lists for dropdowns
            ViewData["AssignedRouteId"] = new SelectList(_routeService.GetAllRoutes(), "RouteId", "RouteName");
            ViewData["DriverId"] = new SelectList(_driverService.GetAllDrivers(), "DriverId", "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            if (!ModelState.IsValid)
                return Page();

            // Server-side validation for BusNumber
            if (Bus.BusNumber <= 0)
            {
                ModelState.AddModelError("Bus.BusNumber", "Bus number must be greater than zero.");
                return Page();
            }

            // Check if the BusNumber already exists
            bool busNumberExists = _busService.CheckBusNumberExists((int)Bus.BusNumber, Bus.BusId);
            if (busNumberExists)
            {
                ModelState.AddModelError("Bus.BusNumber", "The Bus Number must be unique.");
                return Page();
            }

            // Set modification date and update the bus
            Bus.ModifiedAt = DateTime.UtcNow;

            try
            {
                _busService.UpdateBus(Bus);
            }
            catch (Exception)
            {
                if (!_busService.BusExists(Bus.BusId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
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
