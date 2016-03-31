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
    /// Type contains the content of a certain category of Event
    /// </summary>
   
    public class Type
    {

        /// <summary>
        /// Attributes of the Type class
        /// </summary>

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Required]
        [StringLength(256)]
        public string TypeDescription { get; set; }


        /// <summary>
        /// 0 Parameter Constructor
        /// </summary>
        public Type()
        {
            TypeName = " ";
            TypeDescription = " ";
        }
    }
}
