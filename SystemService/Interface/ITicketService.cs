using BusinessObject.Entity;
using System.Collections.Generic;

namespace SystemService
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetTicketById(int ticketId);
        void AddTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int ticketId);
    }
}
