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
      
        
        public int ReviewID { get; set; }
        public int Rating { get; set; }

        [MinLength(1)]
        public string Comment { get; set; }

        
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

     
        public int BusinessID { get; set; }
        public virtual Business Business { get; set; }

        public Review()
        {
            Customer = new Customer();
            Business = new Business();
            Comment = " ";
            Rating = 0;
        }
    }
}
