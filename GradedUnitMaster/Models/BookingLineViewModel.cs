using System;
using System.Linq.Expressions;
using What2Do.Data;
namespace GradedUnitMaster.Models
{
    public class BookingLineViewModel
    {
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public int BookingDate { get; set; }
        
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