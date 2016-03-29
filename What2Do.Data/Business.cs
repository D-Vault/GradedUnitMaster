using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
   
    public class Business : Account
    {
      
        public int BusinessID { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
