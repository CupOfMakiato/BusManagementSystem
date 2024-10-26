using BusinessObject.Entity;
using System.Collections.Generic;
using SystemRepository;

namespace SystemService
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<Ticket> GetAllTickets() => _ticketRepository.GetAllTickets();

        public Ticket GetTicketById(int ticketId) => _ticketRepository.GetTicketById(ticketId);

        public void AddTicket(Ticket ticket) => _ticketRepository.AddTicket(ticket);

        public void UpdateTicket(Ticket ticket) => _ticketRepository.UpdateTicket(ticket);

        public void DeleteTicket(int ticketId) => _ticketRepository.DeleteTicket(ticketId);
    }
}
