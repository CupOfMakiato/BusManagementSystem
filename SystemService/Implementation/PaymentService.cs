using BusinessObject.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void AddPayment(Payment payment) => _paymentRepository.AddPayment(payment);

        public Payment GetPaymentById(int paymentId) => _paymentRepository.GetPaymentById(paymentId);

        public List<PaymentDetail> GetPaymentDetails(int paymentId) => _paymentRepository.GetPaymentDetails(paymentId);
        public string GenerateVnpayUrl(decimal amount)
        {
            // Tạo URL đến VNPAY với các tham số cần thiết
            // Bao gồm số tiền và thông tin trả về sau khi thanh toán
            var baseUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            var returnUrl = "https://yourdomain.com/Member/Payment?handler=PaymentResult";

            // Tạo URL với các tham số cần thiết
            return $"{baseUrl}?amount={amount}&returnUrl={returnUrl}&..."; // các tham số còn lại
        }

        public PaymentResult GetPaymentResult(IQueryCollection query)
        {
            // Kiểm tra thông tin thanh toán trả về từ VNPAY
            // Trích xuất các tham số cần thiết từ query để xác định trạng thái giao dịch
            bool isSuccess = query["vnp_ResponseCode"] == "00"; // Mã 00 là thanh toán thành công
            return new PaymentResult { Success = isSuccess };
        }


    }
    public class PaymentResult
    {
        public bool Success { get; set; }
    }
}
