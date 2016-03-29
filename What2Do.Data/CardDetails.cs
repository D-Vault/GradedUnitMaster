using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    
    public class CardDetails
    {
        
        
        public int DetailID { get; set; }
       
        public int CardNo { get; set; }
      
        public int SortCode { get; set; }
       
        public virtual Customer Customer { get; set; }

        [StringLength(75)]
        public string CardHolder { get; set; }
       
        public int SecurityCode { get; set; }

        public CardDetails()
        {
            CardNo = 0;
            SortCode = 0;
            CardHolder = " ";
            SecurityCode = 0;
        }
    }
}
