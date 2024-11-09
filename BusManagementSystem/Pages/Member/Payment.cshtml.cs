using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Member
{
    public class PaymentModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PayOS _payOS;
        [BindProperty(SupportsGet = true)]
        public decimal Amount { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        public PaymentModel(IBookingService bookingService, IHttpContextAccessor httpContextAccessor, PayOS payOS)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
            _payOS = payOS;
        }

        /*public IActionResult OnGet(int bookingId) // Assuming you pass BookingId as a parameter
        {
            // Ensure the amount is valid
            if (Amount <= 0)
            {
                Console.WriteLine("Invalid amount detected.");
                return RedirectToPage("/Member/Checkout");
            }

            // VNPAY configuration (replace with your actual values)
            string vnp_TmnCode = "089C8IKL"; // Terminal ID from VNPAY
            string vnp_HashSecret = "WN9JUU0IUMVRFIZT6DLETTPWWR9G69C2"; // Secret key from VNPAY
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // VNPAY Sandbox URL
            string vnp_ReturnUrl = "https://3415-125-235-238-110.ngrok-free.app/"; // Your valid URL

            // Use BookingId as the unique order reference
            string orderId = bookingId.ToString();

            // Create parameters for VNPAY
            var vnp_Params = new Dictionary<string, string>
    {
        { "vnp_Version", "2.1.0" },
        { "vnp_Command", "pay" },
        { "vnp_TmnCode", vnp_TmnCode },
        { "vnp_Amount", ((int)(Amount * 100)).ToString() }, // Amount in VND
        { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
        { "vnp_CurrCode", "VND" },
        { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1" },
        { "vnp_Locale", "vn" },
        { "vnp_OrderInfo", "Payment for Order #" + orderId },
        { "vnp_OrderType", "billpayment" },
        { "vnp_ReturnUrl", vnp_ReturnUrl },
        { "vnp_TxnRef", orderId } // Use BookingId as vnp_TxnRef
    };

            // Sort and create a query string
            var sortedParams = new SortedList<string, string>(vnp_Params);
            StringBuilder query = new StringBuilder();
            foreach (var kvp in sortedParams)
            {
                query.AppendFormat("{0}={1}&", kvp.Key, HttpUtility.UrlEncode(kvp.Value));
            }

            // Trim trailing '&'
            string rawData = query.ToString().TrimEnd('&');

            // Generate HMAC SHA-512 hash for the request (do not append vnp_HashSecret here)
            string vnp_SecureHash = CreateHMACSHA512(vnp_HashSecret, rawData);

            // Append vnp_SecureHash to the query
            query.Append("&vnp_SecureHash=" + vnp_SecureHash);

            // Construct the payment URL
            string paymentUrl = vnp_Url + "?" + query.ToString();

            // Optional log for debugging
            Console.WriteLine("Generated payment URL: " + paymentUrl);

            // Redirect to VNPAY's payment gateway
            return Redirect(paymentUrl);
        }


        private static string CreateHMACSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
            }
        }*/

        public void OnGet()
        {

            string bookID = HttpContext.Request.Query["bookingId"];
            string amount = HttpContext.Request.Query["amount"];
            ID = int.Parse(bookID);
            Amount = decimal.Parse(amount);
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var booking = _bookingService.GetBookingById(ID);
                if (booking == null)
                {
                    return NotFound("Booking not found!");
                }

                ItemData item = new ItemData("Vé xe bus tháng", 1, (int)Amount);
                List<ItemData> items = new List<ItemData> { item};

                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";

                PaymentData paymentData = new PaymentData(
                    booking.BookingId, 
                    (int)booking.Amount,
                    "Thanh toán đơn hàng", 
                    items,
                    $"{baseUrl}/Member/Cancel",
                    $"{baseUrl}/Member/Success"
                    );

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                return Redirect(createPayment.checkoutUrl);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}