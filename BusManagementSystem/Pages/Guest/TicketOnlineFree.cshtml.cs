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
            //if (!ModelState.IsValid)
            //{
            //    return Page(); // Return the page if the data is invalid
            //}

            using (var memoryStream = new MemoryStream())
            {
                // Assume you want to store images in the "uploads" folder
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(uploads, file.FileName);
                        // Convert file to byte array and assign to the appropriate property
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            var fileBytes = memoryStream.ToArray();

                            // Assign byte arrays to the appropriate properties
                            if (file.FileName.Contains("CMTND")) // If filename contains "CMTND"
                            {
                                if (FreeTicket.IdfrontImage == null)
                                {
                                    FreeTicket.IdfrontImage = fileBytes;
                                }
                                else
                                {
                                    FreeTicket.IdbackImage = fileBytes;
                                }
                            }
                            else if (file.FileName.Contains("portrait")) // If filename contains "portrait"
                            {
                                FreeTicket.Portrait3x4Image = fileBytes;
                            }
                            else if (file.FileName.Contains("proof")) // If filename contains "proof"
                            {
                                if (FreeTicket.ProofFrontImage == null)
                                {
                                    FreeTicket.ProofFrontImage = fileBytes;
                                }
                                else
                                {
                                    FreeTicket.ProofBackImage = fileBytes;
                                }
                            }
                        }
                    }
                }

                FreeTicketPortrait3x4Image?.CopyTo(memoryStream);
                FreeTicket.Portrait3x4Image = memoryStream.ToArray();
                memoryStream.SetLength(0);

                FreeTicketProofFrontImage?.CopyTo(memoryStream);
                FreeTicket.ProofFrontImage = memoryStream.ToArray();
                memoryStream.SetLength(0);

                // Redirect to a success page
                return RedirectToPage("/Guest/SuccessFreeTicket"); // Change "./Success" to your actual success page
            }

            // Save FreeTicket data
            _freeTicketService.AddFreeTicket(FreeTicket);

            return RedirectToPage("SuccessPage");
        }

    }
}
