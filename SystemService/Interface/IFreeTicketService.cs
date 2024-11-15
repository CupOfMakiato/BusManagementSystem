    using BusinessObject.Entity;
using Microsoft.AspNetCore.Http;
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
            void AddFreeTicket(FreeTicket freeTicket);
            void UpdateFreeTicket(FreeTicket freeTicket);
            void DeleteFreeTicket(int id);
            Task<string> SaveTicketImageAsync(IFormFile file);
            Task<byte[]> GetTicketImageAsync(string fileName);
            //bool IsIdNumberExistingWithStatus(string idNumber, int status);
            bool IsIdNumberExisting(string idNumber);
            void VerifyFreeTicket(FreeTicket freeTicket);
            Task<Ticket> GetOrCreateTicketAsync(string idNumber);
        }
    }
