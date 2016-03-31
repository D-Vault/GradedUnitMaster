using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    
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

        
        public virtual DateTime EventBookingDate { get; set; }
        
        public virtual Event Event { get; set; }
        public virtual Booking Booking { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public BookingLine()
        {
            EventBookingDate = DateTime.Now;
            Event = new Event();
            Booking = new Booking();
        }
    }
}