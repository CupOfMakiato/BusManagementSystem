using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using SystemRepository.Implementation;
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

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ID number is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "ID number must be exactly 12 digits.")]
        public string Idnumber { get; set; }
        [BindProperty]
        public FreeTicket FreeTicket { get; set; } = new FreeTicket();
        [BindProperty]
        public IFormFile? IdfrontImage { get; set; }
        [BindProperty]
        public IFormFile? IdbackImage { get; set; }
        [BindProperty]
        public IFormFile? Portrait3x4Image { get; set; }
        [BindProperty]
        public IFormFile? ProofFrontImage { get; set; }
        [BindProperty]
        public IFormFile? ProofBackImage { get; set; }

        public void OnGet()
        {
            // Set default IssueDate to today
            FreeTicket.IssueDate = DateOnly.FromDateTime(DateTime.Today);

            // Set ValidUntil to 30 days from IssueDate
            FreeTicket.ValidUntil = FreeTicket.IssueDate.AddDays(30);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the ID number already exists with status = 0 (expired)
            if (_freeTicketService.IsIdNumberExisting(FreeTicket.Idnumber))
            {
                ModelState.AddModelError("FreeTicket.Idnumber", "This ID number has already been used! Please check your Ticket expiration date.");
                return Page();
            }

            // Validate DateOfBirth if RecipientType is "Người cao tuổi"
            if (FreeTicket.RecipientType == "Người cao tuổi")
            {
                int age = DateTime.Today.Year - FreeTicket.DateOfBirth.Year;
                if (FreeTicket.DateOfBirth > DateOnly.FromDateTime(DateTime.Today).AddYears(-age)) age--;

                if (age < 60)
                {
                    ModelState.AddModelError("FreeTicket.DateOfBirth", "The Recipient must be 60+ years old!");
                }
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                // Retrieve or create a Ticket record
                var ticket = await _freeTicketService.GetOrCreateTicketAsync(FreeTicket.Idnumber);

                // Assign the TicketId to the FreeTicket instance
                FreeTicket.TicketId = ticket.TicketId;

                var newFreeTicket = new FreeTicket
                {
                    RecipientName = FreeTicket.RecipientName,
                    Gender = FreeTicket.Gender,
                    DateOfBirth = FreeTicket.DateOfBirth,
                    Idnumber = FreeTicket.Idnumber,
                    District = FreeTicket.District,
                    Ward = FreeTicket.Ward,
                    RecipientType = FreeTicket.RecipientType,
                    Phone = FreeTicket.Phone,
                    Email = FreeTicket.Email,
                    TicketDeliveryAddress = FreeTicket.TicketDeliveryAddress,
                    IssueDate = FreeTicket.IssueDate,
                    ValidUntil = FreeTicket.ValidUntil,
                    Status = 1, // Set status to 1 (pending)
                    TicketId = ticket.TicketId // Set the valid TicketId
                };

                // Copy image files to FreeTicket properties
                if (IdfrontImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await IdfrontImage.CopyToAsync(ms);
                        newFreeTicket.IdfrontImage = ms.ToArray();
                    }
                }
                if (IdbackImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await IdbackImage.CopyToAsync(ms);
                        newFreeTicket.IdbackImage = ms.ToArray();
                    }
                }
                if (Portrait3x4Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Portrait3x4Image.CopyToAsync(ms);
                        newFreeTicket.Portrait3x4Image = ms.ToArray();
                    }
                }
                if (ProofBackImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await ProofBackImage.CopyToAsync(ms);
                        newFreeTicket.ProofBackImage = ms.ToArray();
                    }
                }
                if (ProofFrontImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await ProofFrontImage.CopyToAsync(ms);
                        newFreeTicket.ProofFrontImage = ms.ToArray();
                    }
                }

                _freeTicketService.AddFreeTicket(newFreeTicket);
                HttpContext.Session.SetString("TempFreeTicket", JsonSerializer.Serialize(newFreeTicket));

                // Redirect to success page
                return RedirectToPage("/Guest/SuccessFreeTicket");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
    
}
