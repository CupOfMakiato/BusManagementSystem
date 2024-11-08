using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDAO _paymentDAO = PaymentDAO.GetInstance();

        public void AddPayment(Payment payment) => _paymentDAO.AddPayment(payment);

        public Payment GetPaymentById(int paymentId) => _paymentDAO.GetPaymentById(paymentId);

        public List<PaymentDetail> GetPaymentDetails(int paymentId) => _paymentDAO.GetPaymentDetails(paymentId);
    }
}
