using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entity;

namespace BusManagementSystem.Pages.ViewRoute
{
    public class DetailsModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public DetailsModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        public BusinessObject.Entity.Route Route { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FirstOrDefaultAsync(m => m.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }
            else
            {
                Route = route;
            }
            return Page();
        }
    }
}
