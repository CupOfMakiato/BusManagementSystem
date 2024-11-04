using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Pages.ViewDrivers
{
    public class DetailsModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public DetailsModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        public Driver Driver { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverId == id);
            if (driver == null)
            {
                return NotFound();
            }
            else
            {
                Driver = driver;
            }
            return Page();
        }
    }
}