using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDAO
{
    public class FreeTicketVerificationDAO
    {
        private static FreeTicketVerificationDAO instance = null;

        private FreeTicketVerificationDAO() { }

        public static FreeTicketVerificationDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new FreeTicketVerificationDAO();
            }
            return instance;
        }
        public List<FreeTicketVerification> GetAllFreeTicketVerifications()
        {
            using var context = new BusManagementSystemContext();
            return context.FreeTicketVerifications.Include(f => f.User).ToList();
        }

        public FreeTicketVerification GetFreeTicketVerificationById(int verificationId)
        {
            using var context = new BusManagementSystemContext();
            return context.FreeTicketVerifications.FirstOrDefault(f => f.VerificationId == verificationId);
        }

        public void AddFreeTicketVerification(FreeTicketVerification verification)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.FreeTicketVerifications.Add(verification);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while adding the FreeTicketVerification.", ex);
            }
        }
    }
}
