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
        private readonly IEmailService _emailService;

        public IndexModel(IFreeTicketService freeTicketService, IEmailService emailService)
        {
            _freeTicketService = freeTicketService;
            _emailService = emailService;
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
        public IActionResult OnPostVerify(int id)
        {
            var freeTicket = _freeTicketService.GetFreeTicketById(id);
            if (freeTicket == null || freeTicket.Status != 1)
            {
                return NotFound();
            }

            freeTicket.Status = 2;
            freeTicket.IssueDate = DateOnly.FromDateTime(DateTime.Now);
            freeTicket.ValidUntil = DateOnly.FromDateTime(DateTime.Now.AddDays(30));

            _freeTicketService.VerifyFreeTicket(freeTicket);

            // Log the start of email sending
            try
            {
                var expirationDate = DateTime.Parse(freeTicket.ValidUntil.ToString());
                _emailService.SendVerificationEmailAsync(
                    freeTicket.RecipientName,
                    freeTicket.Email,
                    expirationDate
                ).Wait(); // Use Wait() if SendVerificationEmailAsync is async
                Console.WriteLine("Verification email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending verification email: {ex.Message}");
            }

            return RedirectToPage();
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
