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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ReviewID { get; set; }


        public int Rating { get; set; }

       
        public string Comment { get; set; }

        
        public int CustomerId { get; set; }
        
        public virtual Customer Customer { get; set; }

        
        public int BusinessId { get; set; }
        
        public virtual Business Business { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Review()
        {
            Customer = new Customer();
            Business = new Business();
            Comment = " ";
            Rating = 0;
        }
    }
}
