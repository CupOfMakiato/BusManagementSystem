using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService.Interface
{
    public interface IFreeTicketService
    {
        List<FreeTicket> GetAllFreeTickets(); 
        FreeTicket GetFreeTicketById(int id);
        Task AddFreeTicketAsync(FreeTicket freeTicket);
        void UpdateFreeTicket(FreeTicket freeTicket);
        void DeleteFreeTicket(int id);
    }
}
