using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemDAO;
using BusinessObject.Entity;
using EntityRoute = BusinessObject.Entity.Route;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace BusManagementSystem.Pages.Member
{
    public class RegisterTicketModel : PageModel
    {
        private readonly BusManagementSystemContext _context;

        public RegisterTicketModel(BusManagementSystemContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        [BindProperty]
        public string TicketType { get; set; }

        [BindProperty]
        public bool IsPriority { get; set; }

        [BindProperty]
        public string? PriorityType { get; set; }

        [BindProperty]
        public decimal CalculatedPrice { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public int? SelectedRouteId { get; set; }

        [BindProperty]
        public IFormFile Photo3x4 { get; set; } // 3x4 ID photo

        [BindProperty]
        public IFormFile? IDCardFront { get; set; } // Front of ID card

        [BindProperty]
        public IFormFile? IDCardBack { get; set; } // Back of ID card

        public List<EntityRoute> Routes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            User = _context.Users.FirstOrDefault(u => u.UserId == userId.Value);

            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            Routes = _context.Routes.ToList();

            // Set default dates
            StartDate = DateTime.Today;
            EndDate = StartDate.AddDays(30);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Define paths for saving files (ensure the directory exists)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Save Photo3x4 file
            string? photo3x4Path = null;
            if (Photo3x4 != null)
            {
                photo3x4Path = Path.Combine("uploads", Photo3x4.FileName);
                var fullPath = Path.Combine(uploadsFolder, Photo3x4.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await Photo3x4.CopyToAsync(stream);
                }
            }

            // Save IDCardFront and IDCardBack files if priority is selected
            string? idCardFrontPath = null;
            string? idCardBackPath = null;
            if (IsPriority && (PriorityType == "Học Sinh, Sinh Viên" || PriorityType == "Công Nhân khu CN"))
            {
                if (IDCardFront != null)
                {
                    idCardFrontPath = Path.Combine("uploads", IDCardFront.FileName);
                    var fullPath = Path.Combine(uploadsFolder, IDCardFront.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await IDCardFront.CopyToAsync(stream);
                    }
                }

                if (IDCardBack != null)
                {
                    idCardBackPath = Path.Combine("uploads", IDCardBack.FileName);
                    var fullPath = Path.Combine(uploadsFolder, IDCardBack.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await IDCardBack.CopyToAsync(stream);
                    }
                }
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            // Create and populate the Ticket object
            var ticket = new Ticket
            {
                UserId = userId.Value,
                Price = CalculatedPrice,
                StartDate = StartDate,
                EndDate = EndDate,
                RouteId = SelectedRouteId,
                Status = 1, // Pending
                IsFreeTicket = false,
                PriorityType = PriorityType,
                Photo3x4 = photo3x4Path,           // Save the 3x4 photo path
                IDCardFront = idCardFrontPath,      // Save the front ID card path
                IDCardBack = idCardBackPath         // Save the back ID card path
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Member/Checkout");
        }
    }
}
