using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Guest
{
    public class TicketOnlineFreeModel : PageModel
    {
        private readonly IFreeTicketService _freeTicketService;

        public TicketOnlineFreeModel(IFreeTicketService freeTicketService)
        {
            _freeTicketService = freeTicketService;
        }

        [BindProperty]
        public FreeTicket FreeTicket { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // Ensure the handler name matches "asp-page-handler" in the form
        public IActionResult OnPostAddFreeTicket(IFormFile FreeTicketIdcardFront, IFormFile FreeTicketIdcardBack, IFormFile FreeTicketPortrait3x4Image, IFormFile FreeTicketProofFrontImage, IFormFile FreeTicketProofBackImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var memoryStream = new MemoryStream())
            {
                // Load each file into memory
                FreeTicketIdcardFront?.CopyTo(memoryStream);
                FreeTicket.IdcardFront = memoryStream.ToArray();
                memoryStream.SetLength(0);

                FreeTicketIdcardBack?.CopyTo(memoryStream);
                FreeTicket.IdcardBack = memoryStream.ToArray();
                memoryStream.SetLength(0);

                FreeTicketPortrait3x4Image?.CopyTo(memoryStream);
                FreeTicket.Portrait3x4Image = memoryStream.ToArray();
                memoryStream.SetLength(0);

                FreeTicketProofFrontImage?.CopyTo(memoryStream);
                FreeTicket.ProofFrontImage = memoryStream.ToArray();
                memoryStream.SetLength(0);

                FreeTicketProofBackImage?.CopyTo(memoryStream);
                FreeTicket.ProofBackImage = memoryStream.ToArray();
            }

            // Save FreeTicket data
            _freeTicketService.AddFreeTicket(FreeTicket);

            return RedirectToPage("SuccessPage");
        }

    }
}
