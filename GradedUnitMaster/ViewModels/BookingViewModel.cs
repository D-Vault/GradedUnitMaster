using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class BookingViewModel
    {
        /// <summary>
        /// Attributes for viewing bookings with the system
        /// </summary>
        public int Id { get; set; }
        public Account customer_account { get; set; }
        public decimal cost { get; set; }
        public string paymentMethod { get; set; }
        public DateTime BookingDate { get; set; }
        public ICollection<BookingLineViewModel> BookingLines { get; set; }

        /// <summary>
        /// Assigns values to the BookingViewModel
        /// </summary>
        public static Expression<Func<Booking, BookingViewModel>>ViewModel
        {
            get
            {
                return b => new BookingViewModel()
                {
                    Id = b.BookingID,
                    customer_account = b.Customer_Account,
                    cost = b.cost,
                    BookingDate = b.BookingDate,
                    BookingLines = b.Bookings.AsQueryable().Select(BookingLineViewModel.ViewModel).ToList()
                };
            }
        }
    }
}