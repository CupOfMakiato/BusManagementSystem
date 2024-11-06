using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using SystemRepository.Implementation;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Guest
{
    public class TicketOnlineFreeModel : PageModel
    {
        private readonly FreeTicketRepository _freeTicketRepository;

        public TicketOnlineFreeModel()
        {
            _freeTicketRepository = new FreeTicketRepository();
        }

        [BindProperty]
        public FreeTicket FreeTicket { get; set; } = new FreeTicket();

        public void OnGet()
        {
            // Được gọi khi trang được tải lần đầu
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page(); // Return the page if the data is invalid
            //}

            // Handle file uploads
            if (Request.Form.Files.Count > 0)
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

                // Assign additional information to FreeTicket
                FreeTicket.IssueDate = DateOnly.FromDateTime(DateTime.Now);
                FreeTicket.ValidUntil = FreeTicket.ValidUntil == null ? null : FreeTicket.ValidUntil;

                // Add the free ticket to the database
                _freeTicketRepository.AddFreeTicket(FreeTicket);

                // Redirect to a success page
                return RedirectToPage("/Guest/SuccessFreeTicket"); // Change "./Success" to your actual success page
            }

            // If no files were uploaded, return the page with a validation message
            ModelState.AddModelError(string.Empty, "Please upload the required files.");
            return Page();
        }

    }
}