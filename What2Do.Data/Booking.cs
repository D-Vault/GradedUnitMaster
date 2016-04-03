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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        /// <summary>
        /// Highlight attribute of database: 
        /// This attribute can be of type business 
        /// as a business customer
        /// </summary>
        public virtual Account Customer_Account { get; set; }

       
        public decimal cost { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

       
        public int PaymentID { get; set; }

      
        public virtual DateTime BookingDate { get; set; }

  
        public virtual ICollection<BookingLine> Bookings { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Booking()
        {
            Customer_Account = new Account();
            cost = 0;
            PaymentMethod = "";
            BookingDate = DateTime.Now;
        }
    }
}
