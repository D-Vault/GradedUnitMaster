using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
   
    public class Location
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int LocationID { get; set; }

        
        [StringLength(60)]
        public string Street { get; set; }

        
        [StringLength(60)]
        public string Town { get; set; }

       
        [StringLength(8)]
        public string Postcode { get; set; }

        public int BusinessID { get; set; }
        public virtual Business Business { get; set; }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public Location()
        {
            Street = "";
            Town = " ";
            Postcode = " ";
;
        }

    }
}