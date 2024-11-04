using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Pages.ViewBus
{
    public class DeleteModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public DeleteModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bus Bus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses
                .Include(b => b.Driver)
                .Include(u => u.AssignedRoute)
                .FirstOrDefaultAsync(m => m.BusId == id);

            if (bus == null)
            {
                return NotFound();
            }
            else
            {
                Bus = bus;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses.FindAsync(id);
            if (bus != null)
            {
                Bus = bus;
                _context.Buses.Remove(Bus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}