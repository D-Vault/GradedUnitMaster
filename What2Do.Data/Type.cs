using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Required]
        [StringLength(256)]
        public string TypeDescription { get; set; }
    }
}
