using BusinessObject.Entity;
using System.Collections.Generic;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class FreeTicketRepository : IFreeTicketRepository
    {
        private readonly FreeTicketDAO _freeTicketDAO;

        public FreeTicketRepository()
        {
            _freeTicketDAO = FreeTicketDAO.GetInstance();
        }

        public List<FreeTicket> GetAllFreeTickets() // Phương thức trả về danh sách FreeTicket
        {
            return _freeTicketDAO.GetAllFreeTickets(); // Gọi phương thức từ DAO
        }

        public FreeTicket GetFreeTicketById(int id) // Phương thức trả về FreeTicket theo ID
        {
            return _freeTicketDAO.GetFreeTicketById(id); // Gọi phương thức từ DAO
        }

        public void AddFreeTicket(FreeTicket freeTicket)
        {
           _freeTicketDAO.AddFreeTicket(freeTicket);
        }

        public void UpdateFreeTicket(FreeTicket freeTicket) // Phương thức cập nhật FreeTicket
        {
            _freeTicketDAO.UpdateFreeTicket(freeTicket); // Gọi phương thức từ DAO
        }

        public void DeleteFreeTicket(int id) // Phương thức xóa FreeTicket theo ID
        {
            _freeTicketDAO.DeleteFreeTicket(id); // Gọi phương thức từ DAO
        }
    }
}
