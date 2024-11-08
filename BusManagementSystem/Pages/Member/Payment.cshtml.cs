using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BusManagementSystem.Pages.Member
{
    public class PaymentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public decimal Amount { get; set; }

        public IActionResult OnGet(int bookingId) // Assuming you pass BookingId as a parameter
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
        }
    }
}