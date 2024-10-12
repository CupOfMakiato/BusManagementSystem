using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Pages.ViewBus
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Entity.BusManagementSystemContext _context;

        public IndexModel(BusinessObject.Entity.BusManagementSystemContext context)
        {
            _context = context;
        }

        public IList<Bus> Bus { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Bus = await _context.Buses
                .Include(b => b.Driver) // Include the Driver entity
                .Include(u => u.AssignedRoute).ToListAsync();
        }
    }
}
