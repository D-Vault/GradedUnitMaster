using System;
using System.Linq.Expressions;
using What2Do.Data;
namespace GradedUnitMaster.Models
{
    /// <summary>
    /// Displays the details of the bookings 
    /// </summary>
    public class BookingLineViewModel
    {
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public int BookingDate { get; set; }
        
        /// <summary>
        /// Assigns values to the booking
        /// </summary>
        public static Expression<Func<BookingLine, BookingLineViewModel>> ViewModel
        {
            get
            {
                return bl => new BookingLineViewModel()
                {
                    BookingId = bl.BookingID,
                    EventId = bl.EventID, 
                    BookingDate = bl.EventDateId

                };
            }
        }
    }
}