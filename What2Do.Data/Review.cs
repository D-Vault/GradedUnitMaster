using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{

    /// <summary>
    /// Review Class contains the data for customer reviews of a business
    /// </summary>
 
    public class Review
    {

        /// <summary>
        /// Attributes of the review class 
        /// </summary>

        
        [Key]
        public int ReviewID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        // This attribute is of Type Account due to the possibility 
        // of a business making a review
        public virtual Account Customer_Account { get; set; }

        public virtual Business Business { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Review()
        {
            Customer_Account = new Account();
            Business = new Business();
            Comment = " ";
            Rating = 0;
        }
    }
}
