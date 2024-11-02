using BusinessObject.Entity;
using System.Collections.Generic;

namespace SystemRepository
{
    public interface ITicketRepository
    {
        List<Ticket> GetAllTickets();
        Ticket GetTicketById(int ticketId);
        void AddTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int ticketId);
    }
}

