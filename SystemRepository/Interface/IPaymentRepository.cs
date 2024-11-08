using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRepository.Interface
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        Payment GetPaymentById(int paymentId);
        List<PaymentDetail> GetPaymentDetails(int paymentId);
    }
}
