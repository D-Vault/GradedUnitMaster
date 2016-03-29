using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
   
    public class Location
    {
      
        
        public int LocationID { get; set; }

        
        [StringLength(60)]
        public string Street { get; set; }

        
        [StringLength(60)]
        public string Town { get; set; }

       
        [StringLength(8)]
        public string Postcode { get; set; }


        public Location()
        {
            Street = "";
            Town = " ";
            Postcode = " ";
;
        }

    }
}