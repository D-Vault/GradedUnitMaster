using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    public class BookingLine
    {

        [ForeignKey("Event")]
        public int EventID { get; set; }

        [ForeignKey("Booking")]
        public int BookingID { get; set; }

        [Required]
        public DateTime EventBookingDate { get; set; }
        
        public Event Event { get; set; }
        public Booking Booking { get; set; }
    }
}