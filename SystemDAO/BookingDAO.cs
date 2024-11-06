using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemDAO
{
    public class BookingDAO
    {
        private static BookingDAO instance = null;

        public BookingDAO() { }

        public static BookingDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new BookingDAO();
            }
            return instance;
        }

        // Retrieve all bookings with associated Ticket, User, and Bus
        public List<Booking> GetAllBookings()
        {
            var list = new List<Booking>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Bookings
                    .Include(b => b.Ticket)
                    .Include(b => b.User)
                    .Include(b => b.Payments)  // Include payments associated with the booking
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        // Retrieve a booking by its ID
        public Booking GetBookingById(int bookingId)
        {
            Booking booking = null;
            try
            {
                using var context = new BusManagementSystemContext();
                booking = context.Bookings
                    .Include(b => b.Ticket)
                    .Include(b => b.User)
                    .Include(b => b.Payments)
                    .FirstOrDefault(b => b.BookingId == bookingId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return booking;
        }

        // Add a new booking
        public void AddBooking(Booking booking)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                booking.CreatedAt = DateTime.Now;  // Set CreatedAt to current time
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding booking: " + ex.Message);
            }
        }

        // Update an existing booking
        public void UpdateBooking(Booking booking)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                var existingBooking = context.Bookings
                    .FirstOrDefault(b => b.BookingId == booking.BookingId);

                if (existingBooking != null)
                {
                    // Update properties
                    existingBooking.UserId = booking.UserId;
                    existingBooking.TicketId = booking.TicketId;
                    existingBooking.BookingDate = booking.BookingDate;
                    existingBooking.Status = booking.Status;
                    existingBooking.ModifiedAt = DateTime.Now;  // Update ModifiedAt
                    existingBooking.ModifiedBy = booking.ModifiedBy;  // Set ModifiedBy

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Booking not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating booking: " + ex.Message);
            }
        }

        // Delete a booking by its ID
        public void DeleteBooking(int bookingId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                var booking = context.Bookings
                    .FirstOrDefault(b => b.BookingId == bookingId);

                if (booking != null)
                {
                    context.Bookings.Remove(booking);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Booking not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting booking: " + ex.Message);
            }
        }

        // Get all bookings for a specific user
        public List<Booking> GetBookingsByUserId(int userId)
        {
            var list = new List<Booking>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Bookings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Ticket)
                    .Include(b => b.User)
                    .Include(b => b.Payments)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        // Get all bookings for a specific ticket
        public List<Booking> GetBookingsByTicketId(int ticketId)
        {
            var list = new List<Booking>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Bookings
                    .Where(b => b.TicketId == ticketId)
                    .Include(b => b.Ticket)
                    .Include(b => b.User)
                    .Include(b => b.Payments)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        // Check if a booking exists for a given user and ticket
        public bool BookingExists(int userId, int ticketId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Bookings
                    .Any(b => b.UserId == userId && b.TicketId == ticketId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if booking exists: " + ex.Message);
            }
        }

    }
}
