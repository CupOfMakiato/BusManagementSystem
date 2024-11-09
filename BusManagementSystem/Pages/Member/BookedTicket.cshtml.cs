using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.payOS;
using Net.payOS.Types;
using System.Text.Json;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Member
{
    public class BookedTicketModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly PayOS _payOS;

        public BookedTicketModel(IBookingService bookingService, PayOS payOS)
        {
            _bookingService = bookingService;
            _payOS = payOS;
        }

        public IList<Booking> Bookings { get; set; } = null;
        public int UserID { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // Session check - Optional for guest
            if (!CheckGuestSession())
                return RedirectToPage("/Login");

            var bookList = _bookingService.GetBookingsByUserId(UserID);

            foreach (var book in bookList)
            {
               await CheckPaymentCompleted(book.BookingId);
            }

            Bookings = bookList;
            return Page();
        }

        public bool CheckGuestSession()
        {
            var loginAccount = HttpContext.Session.GetString("LoginSession");
            if (loginAccount != null)
            {
                try
                {
                    var account = JsonSerializer.Deserialize<User>(loginAccount);
                    // No specific role check for guests
                    if (account != null)
                        UserID = account.UserId;
                    return true;
                }
                catch (JsonException)
                {
                    // Handle potential deserialization issues
                    return false;
                }
            }
            return true; // Allow access even if session is not present for guest
        }

        public async Task<PageResult> CheckPaymentCompleted(int bookID)
        {
            try
            {
            var book = _bookingService.GetBookingById(bookID);
            var check = await _payOS.getPaymentLinkInformation(bookID);
            if (check != null && check.status == "PAID")
            {
                book.Status = 2;
                _bookingService.UpdateBooking(book);
            }
            return Page();

            }catch (Exception ex)
            {
                return Page();
            }
        }
    }
}