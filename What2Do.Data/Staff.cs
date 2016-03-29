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
    /// Staff Class is a subtype of account which contains the details of staff members
    /// </summary>
    
    public class Staff : Account
    {

        /// <summary>
        /// Attributes of the Staff Class
        /// </summary>
     
        public int StaffID { get; set; }

        [StringLength(50)]
        public string StaffRole { get; set; }

        /// <summary>
        /// 0 Parameter constructor 
        /// </summary>
        public Staff()
        {
            StaffRole = " ";
        }
    }
}
