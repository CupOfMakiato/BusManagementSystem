using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        public void AddBooking(Booking booking)
        {
            BookingDAO.GetInstance().AddBooking(booking);
        }

        public bool BookingExists(int userId, int ticketId)
        {
            return BookingDAO.GetInstance().BookingExists(userId, ticketId);
        }

        public void DeleteBooking(int bookingId)
        {
            BookingDAO.GetInstance().DeleteBooking(bookingId);
        }

        public List<Booking> GetAllBookings()
        {
            return BookingDAO.GetInstance().GetAllBookings();
        }

        public Booking GetBookingById(int bookingId)
        {
            return BookingDAO.GetInstance().GetBookingById(bookingId);
        }

        public List<Booking> GetBookingsByTicketId(int ticketId)
        {
            return BookingDAO.GetInstance().GetBookingsByTicketId((int)ticketId);
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            return BookingDAO.GetInstance().GetBookingsByUserId(userId);
        }

        public void UpdateBooking(Booking booking)
        {
            BookingDAO.GetInstance().UpdateBooking(booking);
        }
    }
}
