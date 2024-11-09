using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using BusinessObject.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using SystemService;

namespace BusManagementSystem.Pages.ViewTicket
{
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketService;

        public IndexModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        public IActionResult OnGet(string? searchQuery)
        {
            // Session check for staff
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Store the search query in the model for the Razor Page


            // Fetch all tickets
            var tickets = _ticketService.GetAllTickets();
            Tickets = tickets;

            return Page();
        }

        public IActionResult OnPostVerify(int id)
        {
            // Get the ticket to be verified
            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null || ticket.Status != 1) // Ensure ticket is in "Pending" status
            {
                return NotFound();
            }

            // Update ticket to verified
            ticket.Status = 2; // Verified
            ticket.StartDate = DateTime.Now;
            ticket.EndDate = DateTime.Now.AddDays(30);

            _ticketService.UpdateTicket(ticket);

            return new JsonResult(new { success = true });
        }

        private bool CheckSession()
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
                    return false;
                }
            }
            return false;
        }
    }
}
