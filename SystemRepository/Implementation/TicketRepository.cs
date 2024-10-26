using BusinessObject.Entity;
using System.Collections.Generic;
using SystemDAO;

namespace SystemRepository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDAO _ticketDAO;

        public TicketRepository()
        {
            _ticketDAO = new TicketDAO();
        }

        public List<Ticket> GetAllTickets() => _ticketDAO.GetAllTickets();

        public Ticket GetTicketById(int ticketId) => _ticketDAO.GetTicketById(ticketId);

        public void AddTicket(Ticket ticket) => _ticketDAO.AddTicket(ticket);

        public void UpdateTicket(Ticket ticket) => _ticketDAO.UpdateTicket(ticket);

        public void DeleteTicket(int ticketId) => _ticketDAO.DeleteTicket(ticketId);
    }
}
