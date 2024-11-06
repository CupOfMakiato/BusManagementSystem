using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Guest
{
    public class TicketOnlineFreeModel : PageModel
    {
        private readonly IFreeTicketService _freeTicketService;

        [BindProperty]
        public FreeTicket FreeTicket { get; set; }

        public TicketOnlineFreeModel(IFreeTicketService freeTicketService)
        {
            _freeTicketService = freeTicketService;
        }

        public void OnGet()
        {
            // Initialize FreeTicket with default values if needed
            FreeTicket = new FreeTicket();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            try
            {
                _freeTicketService.AddFreeTicket(FreeTicket);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Log the error and return to the form with an error message
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
