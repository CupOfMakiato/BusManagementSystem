using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRepository.Interface
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        Booking GetBookingById(int bookingId);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int bookingId);
        List<Booking> GetBookingsByUserId(int userId);
        List<Booking> GetBookingsByTicketId(int ticketId);
        bool BookingExists(int userId, int ticketId);
    }
}
