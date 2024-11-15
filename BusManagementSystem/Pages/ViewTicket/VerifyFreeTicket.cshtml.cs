using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.ViewTicket
{
    public class VerifyFreeTicketModel : PageModel
    {
        private readonly IFreeTicketService _freeTicketService;
        private readonly IEmailService _emailService;

        public VerifyFreeTicketModel(IFreeTicketService freeTicketService, IEmailService emailService)
        {
            _freeTicketService = freeTicketService;
            _emailService = emailService;
        }
        public IList<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();
        public IActionResult OnGet(int? id)
        {
            // Session check for staff
            if (!CheckSession())
                return RedirectToPage("/Login");

            // Fetch all free tickets
            var freeTickets = _freeTicketService.GetAllFreeTickets().FirstOrDefault(t => t.FreeTicketId == id);
            return Page();
        }
        public IActionResult OnPostVerify(int id)
        {
            // Get the free ticket to be verified
            var freeTicket = _freeTicketService.GetFreeTicketById(id);
            if (freeTicket == null || freeTicket.Status != 1) // Ensure ticket is in "Pending" status
            {
                return NotFound();
            }

            // Update free ticket to verified
            freeTicket.Status = 2; // Set status to 2 (Verified)
            freeTicket.IssueDate = DateOnly.FromDateTime(DateTime.Now);
            freeTicket.ValidUntil = DateOnly.FromDateTime(DateTime.Now.AddDays(30)); // Set expiration date 30 days from now

            // Verify free ticket and associated ticket in the database
            _freeTicketService.VerifyFreeTicket(freeTicket);

            // Send verification email
            var expirationDate = DateTime.Parse(freeTicket.ValidUntil.ToString());
            _emailService.SendVerificationEmailAsync(
                freeTicket.RecipientName,
                freeTicket.Email,
                expirationDate
            );

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
