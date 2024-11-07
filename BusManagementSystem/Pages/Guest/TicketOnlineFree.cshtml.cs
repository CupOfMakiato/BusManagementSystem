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
            // Validate DateOfBirth if RecipientType is "Người cao tuổi"
            if (FreeTicket.RecipientType == "Người cao tuổi")
            {
                int age = DateTime.Today.Year - FreeTicket.DateOfBirth.Year;
                if (FreeTicket.DateOfBirth > DateOnly.FromDateTime(DateTime.Today).AddYears(-age)) age--;

                if (age < 60)
                {
                    ModelState.AddModelError("FreeTicket.DateOfBirth", "Người cao tuổi phải từ 60 tuổi trở lên.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ticket = new FreeTicket
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
            };

            try
            {
                if (IdfrontImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await IdfrontImage.CopyToAsync(ms);
                        ticket.IdfrontImage = ms.ToArray();
                    }
                }
                if (IdbackImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await IdbackImage.CopyToAsync(ms);
                        ticket.IdbackImage = ms.ToArray();
                    }
                }
                if (Portrait3x4Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await Portrait3x4Image.CopyToAsync(ms);
                        ticket.Portrait3x4Image = ms.ToArray();
                    }
                }
                if (ProofBackImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await ProofBackImage.CopyToAsync(ms);
                        ticket.ProofBackImage = ms.ToArray();
                    }
                }
                if (ProofFrontImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await ProofFrontImage.CopyToAsync(ms);
                        ticket.ProofFrontImage = ms.ToArray();
                    }
                }

                _freeTicketService.AddFreeTicket(ticket);
                HttpContext.Session.SetString("TempFreeTicket", JsonSerializer.Serialize(ticket));

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
