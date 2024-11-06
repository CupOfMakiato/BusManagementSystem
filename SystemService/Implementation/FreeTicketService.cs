using BusinessObject.Entity;
using System.Collections.Generic;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class FreeTicketService : IFreeTicketService
    {
        private readonly IFreeTicketRepository _freeTicketRepository;

        public FreeTicketService(IFreeTicketRepository freeTicketRepository)
        {
            _freeTicketRepository = freeTicketRepository;
        }

        public List<FreeTicket> GetAllFreeTickets() // Phương thức trả về danh sách FreeTicket
        {
            return _freeTicketRepository.GetAllFreeTickets(); // Gọi phương thức từ Repository
        }

        public FreeTicket GetFreeTicketById(int id) // Phương thức trả về FreeTicket theo ID
        {
            return _freeTicketRepository.GetFreeTicketById(id); // Gọi phương thức từ Repository
        }

        public  void AddFreeTicket(FreeTicket freeTicket)
        {
             _freeTicketRepository.AddFreeTicket(freeTicket);
        }


        public void UpdateFreeTicket(FreeTicket freeTicket) // Phương thức cập nhật FreeTicket
        {
            _freeTicketRepository.UpdateFreeTicket(freeTicket); // Gọi phương thức từ Repository
        }

        public void DeleteFreeTicket(int id) // Phương thức xóa FreeTicket theo ID
        {
            _freeTicketRepository.DeleteFreeTicket(id); // Gọi phương thức từ Repository
        }
    }
}
