using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemDAO;
using BusinessObject.Entity;
using System.Text.Json;

namespace BusManagementSystem.Pages.Member
{
    public class CheckoutModel : PageModel
    {
        private readonly BusManagementSystemContext _context;

        public CheckoutModel(BusManagementSystemContext context)
        {
            _context = context;
        }

        public decimal Price { get; set; }
        public string RouteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IActionResult OnGet()
        {
            var tempTicketJson = HttpContext.Session.GetString("TempTicket");
            if (string.IsNullOrEmpty(tempTicketJson))
            {
                return RedirectToPage("/Member/RegisterTicket");
            }

            var tempTicket = JsonSerializer.Deserialize<Ticket>(tempTicketJson);
            var route = _context.Routes.Find(tempTicket.RouteId);

            Price = tempTicket.Price ?? 0;
            StartDate = tempTicket.StartDate ?? DateTime.Now;
            EndDate = tempTicket.EndDate ?? DateTime.Now;
            RouteName = route?.RouteName ?? "Unknown Route";

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = 1; // Replace with logged-in user ID

            var booking = new Booking
            {
                UserId = userId,
                BookingDate = DateTime.Now,
                Status = 1 // Pending
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToPage("/Member/Payment");
        }
    }
}
