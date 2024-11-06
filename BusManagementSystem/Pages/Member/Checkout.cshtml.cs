using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemDAO;
using SystemService;
using SystemService.Interface;
using System.Linq;

namespace BusManagementSystem.Pages.Member
{
    public class CheckoutModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IRouteService _routeService;

        public CheckoutModel(IBookingService bookingService, IUserService userService, IRouteService routeService, ITicketService ticketService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _routeService = routeService;
            _ticketService = ticketService;
        }

        public decimal Price { get; set; }
        public string RouteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public IList<BusinessObject.Entity.Route> Routes { get; set; } = new List<BusinessObject.Entity.Route>();
        public IList<User> Users { get; set; } = new List<User>();
        public IList<Booking> Bookings { get; set; } = new List<Booking>();

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

            // Retrieve Route details from the database using LINQ to find the route by ID
            var route = _routeService.GetAllRoutes().FirstOrDefault(r => r.RouteId == tempTicket.RouteId);
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

            _bookingService.AddBooking(booking);

            // Redirect to Payment page after booking is saved
            return RedirectToPage("/Member/Payment");
        }

        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                try
                {
                    var account = JsonSerializer.Deserialize<User>(loginAccount);
                    if (account != null && account.RoleId == 3) // Member role check
                        return true;
                }
                catch (JsonException)
                {
                    // Handle potential deserialization issues
                    return false;
                }
            }
            return false;
        }
    }
}
