using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    
    public class BookingLine
    {

       [Key]
        public int EventID { get; set; }

        public int BookingID { get; set; }

        
        public virtual DateTime EventBookingDate { get; set; }
        
        public virtual Event Event { get; set; }
        public virtual Booking Booking { get; set; }

        public BookingLine()
        {
            EventBookingDate = DateTime.Now;
            Event = new Event();
            Booking = new Booking();
        }
    }
}