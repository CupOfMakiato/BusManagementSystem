using BusinessObject.Entity;
using SystemRepository;
using SystemRepository.Interface;

namespace SystemService
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IRouteRepository _routeRepository;

        public TicketService(ITicketRepository ticketRepository, IRouteRepository routeRepository)
        {
            _ticketRepository = ticketRepository;
            _routeRepository = routeRepository;
        }

        public List<Ticket> GetAllTickets() => _ticketRepository.GetAllTickets();

        public Ticket GetTicketById(int ticketId) => _ticketRepository.GetTicketById(ticketId);

        public void AddTicket(Ticket ticket) => _ticketRepository.AddTicket(ticket);

        public void UpdateTicket(Ticket ticket) => _ticketRepository.UpdateTicket(ticket);

        public void DeleteTicket(int ticketId) => _ticketRepository.DeleteTicket(ticketId);

        public Ticket CalculateTicketPrice(Ticket ticket)
        {
            if (ticket.TicketType == "Một tuyến")
            {
                ticket.Price = 100000;
            }
            else if (ticket.TicketType == "Liên Tuyến")
            {
                ticket.Price = 200000;
            }

            if (ticket.PriorityType != null)
            {
                ticket.Price *= 0.5m;
            }
            return ticket;
        }

        public List<Route> GetAllRoutes() => _routeRepository.GetAllRoutes();
    }
}