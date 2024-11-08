using BusinessObject.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SystemDAO
{
    public class PaymentDAO
    {
        // Các phương thức khác trong DAO
        private static PaymentDAO instance = null;

        private PaymentDAO() { }

        public static PaymentDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new PaymentDAO();
            }
            return instance;
        }
        public void AddPayment(Payment payment)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Payments.Add(payment);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while processing the payment.", ex);
            }
        }
        public List<PaymentDetail> GetPaymentDetails(int paymentId)
        {
            using var context = new BusManagementSystemContext();
            return context.PaymentDetails.Where(d => d.PaymentId == paymentId).ToList();
        }
        public Payment GetPaymentById(int paymentId)
        {
            using var context = new BusManagementSystemContext();
            return context.Payments
                          .Include(p => p.PaymentDetails)
                          .FirstOrDefault(p => p.PaymentId == paymentId);
        }

        //public PaymentResult GetPaymentResult(IQueryCollection query)
        //{
        //    // Bắt các tham số phản hồi từ VNPAY
        //    var transactionStatus = query["vnp_ResponseCode"];
        //    var paymentId = query["vnp_TxnRef"];
        //    var amount = query["vnp_Amount"];
        //    var bankCode = query["vnp_BankCode"];

        //    // Kiểm tra trạng thái giao dịch
        //    if (transactionStatus == "00") // "00" là mã thành công của VNPAY
        //    {
        //        return new PaymentResult
        //        {
        //            PaymentId = int.Parse(paymentId),
        //            Status = "Success",
        //            Amount = decimal.Parse(amount) / 100, // VNPAY trả về số tiền với đơn vị 100 VND
        //            BankCode = bankCode
        //        };
        //    }
        //    else
        //    {
        //        return new PaymentResult
        //        {
        //            PaymentId = int.Parse(paymentId),
        //            Status = "Failure",
        //            Amount = decimal.Parse(amount) / 100,
        //            BankCode = bankCode
        //        };
        //    }
        //}
    }

    //public class PaymentResult
    //{
    //    public int PaymentId { get; set; }
    //    public string Status { get; set; }
    //    public decimal Amount { get; set; }
    //    public string BankCode { get; set; }
    //}
}