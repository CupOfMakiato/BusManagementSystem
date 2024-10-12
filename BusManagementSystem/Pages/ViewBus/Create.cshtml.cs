using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusManagementSystem.Pages.ViewBus
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Entity.BusManagementSystemContext _context;

        public CreateModel(BusinessObject.Entity.BusManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AssignedRouteId"] = new SelectList(_context.Buses, "AssignedRouteId", "AssignedRouteId");
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

            _context.Buses.Add(Bus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
