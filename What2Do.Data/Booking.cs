using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
 
    public class Booking
    {
        /// <summary>
        /// Attributes for the booking class along with their getters and setters
        /// </summary>

        
        public int BookingID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

       
        public float cost { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

       
        public int PaymentID { get; set; }

      
        public DateTime BookingDate { get; set; }

  
        public virtual ICollection<BookingLine> Bookings { get; set; }

        public Booking()
        {
            Customer = new Customer();
            cost = 0.00f;
            PaymentMethod = "";
            BookingDate = DateTime.Now;
        }
    }
}
