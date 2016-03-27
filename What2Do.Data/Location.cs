using System.ComponentModel.DataAnnotations;

namespace What2Do.Data
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        [Required]
        [StringLength(60)]
        public string Street { get; set; }
        [Required]
        [StringLength(60)]
        public string Town { get; set; }
        [Required]
        [StringLength(8)]
        public string Postcode { get; set; }
    }
}