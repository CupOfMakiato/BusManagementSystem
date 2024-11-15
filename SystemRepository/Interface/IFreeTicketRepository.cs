using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRepository.Interface
{
    public interface IFreeTicketRepository
    {
        List<FreeTicket> GetAllFreeTickets();
        FreeTicket GetFreeTicketById(int id);
        void AddFreeTicket(FreeTicket freeTicket);
        void UpdateFreeTicket(FreeTicket freeTicket);
        void DeleteFreeTicket(int id);
        bool IsIdNumberExisting(string idNumber);
        //bool IsIdNumberExistingWithStatus(string idNumber, int status);
        void VerifyFreeTicket(FreeTicket freeTicket);
        Task<Ticket> GetOrCreateTicketAsync(string idNumber);
    }
}
