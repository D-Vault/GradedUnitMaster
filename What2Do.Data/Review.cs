using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        [MinLength(1)]
        public string Comment { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Business")]
        public int BusinessID { get; set; }
        public Business Business { get; set; }
    }
}
