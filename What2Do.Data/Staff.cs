using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    class Staff : Account
    {
        public int StaffID { get; set; }

        [StringLength(50)]
        public string StaffRole { get; set; }
    }
}
