using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
