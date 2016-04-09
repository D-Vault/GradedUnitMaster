using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
  [Table("Account")]
    public class Account : ApplicationUser
    {

        /// <summary>
        /// Attributes for Accounts in Database
        /// </summary>

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CardDetails>Cards { get; set; }
        public virtual ICollection<Review>Reviews { get; set; }

       
        [StringLength(75)]
        public string FirstName { get; set; }

        [StringLength(75)]
        public string Surname { get; set; }

        [StringLength(60)]
        public string Street { get; set; }

        [StringLength(60)]
        public string Town { get; set; }

        [StringLength(8)]
        public string Postcode { get; set; }

        [StringLength(11)]
        public string TelNo { get; set; }

        [StringLength(11)]
        public string MobileNo { get; set; }

        public Account()
        {
            FirstName = " ";
            Surname = " ";
            Street = " ";
            Town = " ";
            Postcode = " ";
            TelNo = " ";
            MobileNo = " ";
        }

    }
}
