using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    class Booking
    {
        /// <summary>
        /// Attributes for the booking class along with their getters and setters
        /// </summary>

        [Key]
        public int BookingID { get; set; }

        [ForeignKey("Account")]
        public int CustomerID { get; set; }

        [Required]
        public float cost { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }


        public int PaymentID { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public virtual ICollection<BookingLine> Bookings { get; set; }


    }
}
