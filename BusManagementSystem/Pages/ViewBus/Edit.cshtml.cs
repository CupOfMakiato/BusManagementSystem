using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Pages.ViewBus
{
    public class EditModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _busService;

        public EditModel(SystemDAO.BusManagementSystemContext busService)
        {
            _busService = busService;
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _busService.Buses.FirstOrDefaultAsync(b => b.BusId == id); ;
            if (bus == null)
            {
                return NotFound();
            }
            Bus = bus;
            ViewData["AssignedRouteId"] = new SelectList(_busService.Routes, "RouteId", "RouteName");

            ViewData["DriverId"] = new SelectList(_busService.Drivers.Where(d => d.Status == 1), "DriverId", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Server-side validation for BusNumber
            if (Bus.BusNumber <= 0)
            {
                ModelState.AddModelError("Bus.BusNumber", "Bus number must be greater than zero.");
                return RedirectToPage("./Edit");
            }

            // Check if the BusNumber already exists in the database
            bool busNumberExists = await _busService.Buses.AnyAsync(b => b.BusNumber == Bus.BusNumber);

            if (busNumberExists)
            {
                // Add a model error if the BusNumber already exists
                ModelState.AddModelError("Bus.BusNumber", "The Bus Number must be unique.");
                return Page();
            }
            //if (!_context.Drivers.Any(d => d.DriverId == Bus.DriverId && d.Status == 1))
            //{
            //    ModelState.AddModelError("Bus.DriverId", "Selected driver is not available.");
            //}

            //if (!_context.Routes.Any(r => r.RouteId == Bus.AssignedRouteId))
            //{
            //    ModelState.AddModelError("Bus.AssignedRouteId", "Selected route is not available.");
            //}

            //if (Bus.Status < 0 || Bus.Status > 2)
            //{
            //    ModelState.AddModelError("Bus.Status", "Invalid status selected.");
            //}

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the ModifiedAt property to the current date and time
            Bus.ModifiedAt = DateTime.UtcNow; // Use UTC to avoid timezone issues

            _busService.Attach(Bus).State = EntityState.Modified;

            try
            {
                await _busService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(Bus.BusId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BusExists(int id)
        {
            return _busService.Buses.Any(e => e.BusId == id);
        }
    }
}