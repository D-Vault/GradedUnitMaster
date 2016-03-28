using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    public class Account : ApplicationUser
    {

        /// <summary>
        /// Attributes for Accounts in Database
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }

        [ForeignKey("CardDetail")]
        public int CardDetailID { get; set; }

        public ICollection<CardDetails>Cards { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Required]
        [StringLength(60)]
        public string Street { get; set; }

        [Required]
        [StringLength(60)]
        public string Town { get; set; }

        [Required]
        [StringLength(8)]
        public string Postcode { get; set; }

        [Required]
        [StringLength(11)]
        public string TelNo { get; set; }

        [Required]
        [StringLength(11)]
        public string MobileNo { get; set; }

    }
}
