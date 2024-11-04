using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService;
using SystemService.Interface;
using EntityRoute = BusinessObject.Entity.Route;

namespace BusManagementSystem.Pages.Member
{
    public class RegisterTicketModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;

        public RegisterTicketModel(ITicketService ticketService, IUserService userService)
        {
            _ticketService = ticketService;
            _userService = userService;
        }

        public User User { get; set; } = new User();

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
        public IFormFile Photo3x4 { get; set; }

        [BindProperty]
        public IFormFile? IDCardFront { get; set; }

        [BindProperty]
        public IFormFile? IDCardBack { get; set; }

        public List<EntityRoute> Routes { get; set; } = new List<EntityRoute>();

        public IActionResult OnGetAsync()
        {
            var userId = (int)HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Log the issue for debugging purposes
                Console.WriteLine("UserId not found in session, redirecting to login.");
                return RedirectToPage("/Login", new { returnUrl = "/Member/RegisterTicket" });
            }

            User = _userService.GetUserById(userId);

            if (User == null)
            {
                Console.WriteLine("User not found in database, redirecting to login.");
                return RedirectToPage("/Login", new { returnUrl = "/Member/RegisterTicket" });
            }

            Routes = _ticketService.GetAllRoutes();

            StartDate = DateTime.Today;
            EndDate = StartDate.AddDays(30);

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Member/RegisterTicket");
            }
            var userId = (int)HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                Console.WriteLine("UserId not found in session during post, redirecting to login.");
                return RedirectToPage("/Login", new { returnUrl = "/Member/RegisterTicket" });
            }

            User = _userService.GetUserById(userId);

            if (User == null)
            {
                Console.WriteLine("User not found in database during post, redirecting to login.");
                return RedirectToPage("/Login", new { returnUrl = "/Member/RegisterTicket" });
            }

            var ticket = new Ticket
            {
                UserId = userId,
                TicketType = TicketType,
                IsPriority = IsPriority,
                StartDate = StartDate,
                EndDate = EndDate,
                RouteId = SelectedRouteId,
                Status = 1, // Pending
                IsFreeTicket = false,
                PriorityType = PriorityType,
            };

            if (Photo3x4 != null)
            {
                using (var ms = new MemoryStream())
                {
                    Photo3x4.CopyToAsync(ms);
                    ticket.Photo3x4 = ms.ToArray();
                }
            }

            if (IsPriority && (PriorityType == "Học Sinh, Sinh Viên" || PriorityType == "Công Nhân khu CN"))
            {
                if (IDCardFront != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        IDCardFront.CopyToAsync(ms);
                        ticket.IDCardFront = ms.ToArray();
                    }
                }

                if (IDCardBack != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        IDCardBack.CopyToAsync(ms);
                        ticket.IDCardBack = ms.ToArray();
                    }
                }
            }
            _ticketService.CalculateTicketPrice(ticket);
            _ticketService.AddTicket(ticket);

            HttpContext.Session.SetString("TempTicket", JsonSerializer.Serialize(ticket));

            return RedirectToPage("/Member/Checkout");
        }
    }
}