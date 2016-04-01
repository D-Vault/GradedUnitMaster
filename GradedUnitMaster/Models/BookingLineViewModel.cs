using System;
using System.Linq.Expressions;
using What2Do.Data;
namespace GradedUnitMaster.Models
{
    public class BookingLineViewModel
    {
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public DateTime EventBookingDate { get; set; }
        public Event Event { get; set; }
        public Booking Booking { get; set; }

        public static Expression<Func<BookingLine, BookingLineViewModel>> ViewModel
        {
            get
            {
                return bl => new BookingLineViewModel()
                {
                    BookingId = bl.BookingID,
                    EventId = bl.EventID,
                    EventBookingDate = bl.EventBookingDate,
                    Event = bl.Event,
                    Booking = bl.Booking
                };
            }
        }
    }
}