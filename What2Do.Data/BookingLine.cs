using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    /// <summary>
    /// Displays the details of each booking
    /// </summary>
    public class BookingLine
    {
        /// <summary>
        /// Attributes
        /// </summary>
        [Key, Column(Order = 0)]
        [ForeignKey("Event")]
        public int EventID { get; set; }


        [Key, Column(Order = 1)]
        [ForeignKey("Booking")]
        public int BookingID { get; set; }

        public string cartId { get; set; }
        
        public int EventDateId { get; set; }
        public EventDates EventBookingDate { get; set; }
        
 
        public virtual Event Event { get; set; }

       
        public virtual Booking Booking { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public BookingLine()
        {
            EventBookingDate = new EventDates();
            Event = new Event();
            Booking = new Booking();
        }
    }
}