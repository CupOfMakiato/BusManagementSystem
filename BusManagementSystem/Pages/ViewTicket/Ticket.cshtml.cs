using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService;

namespace BusManagementSystem.Pages.ViewTicket
{
    public class TicketModel : PageModel
    {

        private readonly ITicketService _ticketService; // Assuming a service layer

        public TicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string? SearchQuery { get; set; }
        [BindProperty]
        public int ticketID { get; set; }
        public IActionResult OnGet(string search)
        {
            if (!CheckSession())
                return RedirectToPage("/Login");

            SearchQuery = search;

            var listTickets = _ticketService.GetAllTickets();

            Tickets = listTickets;
            return Page();
        }

        public IActionResult OnPost()
        {
            var ticket = _ticketService.GetTicketById(ticketID);
            if (ticket == null)
            {
                return NotFound("Ticket not found!");
            }
            ticket.Status = 2;
            _ticketService.UpdateTicket(ticket);
            return RedirectToPage("/ViewTicket/Ticket");
        }
        public bool CheckSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                try
                {
                    var account = JsonSerializer.Deserialize<User>(loginAccount);
                    if (account != null && account.RoleId == 2) // Staff role check
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
