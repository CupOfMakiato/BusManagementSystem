using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemDAO;

namespace BusManagementSystem.Pages.Member
{
    public class CheckoutModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public CheckoutModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        public decimal Price { get; set; }
        public string RouteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IActionResult OnGet()
        {
            // Retrieve ticket data from session
            var tempTicketJson = HttpContext.Session.GetString("TempTicket");
            if (string.IsNullOrEmpty(tempTicketJson))
            {
                // Redirect to RegisterTicket if TempTicket data is missing
                return RedirectToPage("/Member/RegisterTicket");
            }

            // Deserialize Ticket object from JSON
            var tempTicket = JsonSerializer.Deserialize<Ticket>(tempTicketJson);
            if (tempTicket == null)
            {
                return RedirectToPage("/Member/RegisterTicket");
            }

            // Retrieve Route details from the database
            var route = _context.Routes.Find(tempTicket.RouteId);
            RouteName = route?.RouteName ?? "Unknown Route";
            Price = tempTicket.Price ?? 0;
            StartDate = tempTicket.StartDate ?? DateTime.Now;
            EndDate = tempTicket.EndDate ?? DateTime.Now;

            return Page();
        }

        public IActionResult OnPost()
        {
            // Retrieve User ID from session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login"); // Redirect if user is not logged in
            }

            // Create a new booking with the retrieved User ID
            var booking = new Booking
            {
                UserId = userId.Value,
                BookingDate = DateTime.Now,
                Status = 1 // Pending status
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Redirect to Payment page after booking is saved
            return RedirectToPage("/Member/Payment");
        }
    }
}