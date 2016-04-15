using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
   /// <summary>
   /// Displays the details of the location
   /// </summary>
    public class Location
    {
        /// <summary>
        /// Attributes
        /// </summary>

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