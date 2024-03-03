using ManageBooking.Application.Common.Interfaces;
using ManageBooking.Application.Common.Ultility;
using ManageBooking.Domain.Entities;
using ManageBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBooking.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Booking entity)
        {
            _db.Bookings.Update(entity);
        }

        public void UpdateStatus(int bookingId, string bookingStatus)
        {
            Booking bookingFromDb = _db.Bookings.FirstOrDefault(u=>u.Id == bookingId);
            if (bookingFromDb is not null)
            {
                bookingFromDb.Status = bookingStatus;
                if(bookingFromDb.Status == SD.StatusCheckedIn)
                {
                    bookingFromDb.ActualCheckInDate = DateTime.Now;
                }
                if(bookingFromDb.Status == SD.StatusCompleted)
                {
                    bookingFromDb.ActualCheckOutDate = DateTime.Now;
                }
            }
        }

        public void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId)
        {
            Booking bookingFromDb = _db.Bookings.FirstOrDefault(u => u.Id == bookingId);
            if(bookingFromDb is not null)
            {
                if(!string.IsNullOrEmpty(sessionId))
                {
                    bookingFromDb.StripeSessionId = sessionId;
                }
                if(!string.IsNullOrEmpty(paymentIntentId))
                {
                    bookingFromDb.StripePaymentIntentId = paymentIntentId;
                    bookingFromDb.PaymentDate = DateTime.Now;
                    bookingFromDb.IsPaymentSuccessful = true;
                }
            }
        }
    }
}
