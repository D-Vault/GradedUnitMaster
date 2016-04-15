using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    /// <summary>
    /// Displays the details of the bookings in cart 
    /// </summary>
    public class BookingCartViewModel
    {
        /// <summary>
        /// Attributes
        /// </summary>
        public List<BookingLine>BookingItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}