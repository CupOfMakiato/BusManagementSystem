using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using BusinessObject.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using SystemService;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewFreeTicket
{
    public class IndexModel : PageModel
    {
        private readonly IFreeTicketService _freeTicketService;

        public IndexModel(IFreeTicketService freeTicketService)
        {
            _freeTicketService = freeTicketService;
        }

        public IList<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();

        public IActionResult OnGet()
        {
            // Session check for staff
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Fetch all free tickets
            var freeTickets = _freeTicketService.GetAllFreeTickets();
            FreeTickets = freeTickets;

            return Page();
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
