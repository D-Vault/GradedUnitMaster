using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    public class BookingLine
    {
        [Key]
        public int EventID { get; set; }
        [ForeignKey("Booking")]
        public int BookingID { get; set; }
        [Required]
        public DateTime EventBookingDate { get; set; }
        [Required]
        public Event Event { get; set; }
    }
}