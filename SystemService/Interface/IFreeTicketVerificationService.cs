using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService.Interface
{
    public interface IFreeTicketVerificationService
    {
        List<FreeTicketVerification> GetAll();
        FreeTicketVerification GetVerificationById(int verificationId); 
        void AddFreeTicketVerification(FreeTicketVerification verification);

    }
}
