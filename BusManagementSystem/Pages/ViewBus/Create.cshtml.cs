using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Pages.ViewBus
{
    public class CreateModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public CreateModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AssignedRouteId"] = new SelectList(_context.Routes, "RouteId", "RouteName"); // Assuming you want to select from Routes

            ViewData["DriverId"] = new SelectList(_context.Drivers.Where(d => d.Status == 1), "DriverId", "Name");
            return Page();
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Bus.BusNumber <= 0)
            {
                ModelState.AddModelError("Bus.BusNumber", "Bus number must be greater than zero.");
                return RedirectToPage("./Create");
            }

            // Check if the BusNumber already exists in the database
            bool busNumberExists = await _context.Buses.AnyAsync(b => b.BusNumber == Bus.BusNumber);

            if (busNumberExists)
            {
                // Add a model error if the BusNumber already exists
                ModelState.AddModelError("Bus.BusNumber", "The Bus Number must be unique.");
                return RedirectToPage("./Create");
            }

            // Set the CreatedAt property to the current date and time
            Bus.CreatedAt = DateTime.UtcNow; // Use UTC to avoid timezone issues

            _context.Buses.Add(Bus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
