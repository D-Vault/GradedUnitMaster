using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    public class CardDetails
    {
        [Key]
        public int DetailID { get; set; }
        [Required]
        public int CardNo { get; set; }
        [Required]
        public int SortCode { get; set; }
        [Required]
        [StringLength(75)]
        public string CardHolder { get; set; }
        [Required]
        public int SecurityCode { get; set; }
    }
}
