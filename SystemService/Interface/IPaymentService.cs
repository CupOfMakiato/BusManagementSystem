using BusinessObject.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;

namespace SystemService.Interface
{
    public interface IPaymentService
    {
        string GenerateVnpayUrl(decimal amount);
        //PaymentResult GetPaymentResult(IQueryCollection query);
        void AddPayment(Payment payment);
        Payment GetPaymentById(int paymentId);
        List<PaymentDetail> GetPaymentDetails(int paymentId);
    }
}
