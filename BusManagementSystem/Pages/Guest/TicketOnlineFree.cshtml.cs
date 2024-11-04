using BusinessObject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SystemService.Interface;

namespace BusManagementSystem.Pages.Guest
{
    public class TicketOnlineFreeModel : PageModel
    {
        private readonly IFreeTicketService _freeTicketService;
        private readonly IFreeTicketVerificationService _freeTicketVerificationService; // Dịch vụ cho FreeTicketVerification

        [BindProperty]
        public FreeTicket FreeTicket { get; set; }

        public TicketOnlineFreeModel(IFreeTicketService freeTicketService, IFreeTicketVerificationService freeTicketVerificationService)
        {
            _freeTicketService = freeTicketService;
            _freeTicketVerificationService = freeTicketVerificationService; // Khởi tạo dịch vụ
        }

        public void OnGet()
        {
            // Khởi tạo giá trị mặc định nếu cần
            FreeTicket = new FreeTicket();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Trả về trang hiện tại nếu dữ liệu không hợp lệ
            }

            try
            {
                // Lưu FreeTicket vào cơ sở dữ liệu
                await _freeTicketService.AddFreeTicketAsync(FreeTicket);

                // Tạo đối tượng FreeTicketVerification
                var verification = new FreeTicketVerification
                {
                    //FreeTicketId = FreeTicket.Id, 
                };

                // Lưu FreeTicketVerification vào cơ sở dữ liệu
                _freeTicketVerificationService.AddFreeTicketVerification(verification);

                return RedirectToPage("Index"); // Chuyển hướng về trang chính sau khi thành công
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra khi lưu dữ liệu: {ex.Message}");
                // Ghi lại lỗi chi tiết để phân tích thêm
                Console.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                return Page(); // Trả về trang hiện tại với lỗi
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                // Ghi lại lỗi chi tiết để phân tích thêm
                Console.WriteLine($"Exception: {ex.Message}");
                return Page(); // Trả về trang hiện tại với lỗi
            }
        }
    }
}
