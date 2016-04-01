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
        /// <summary>
        /// Attributes
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DetailID { get; set; }
       
        public int CardNo { get; set; }
      
        public int SortCode { get; set; }
        
        public int ExpireMonth { get; set; }

        public int ExpireYear { get; set; }
       
        public string Type { get; set; }

        public virtual Account Account { get; set; }

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

        /* Now create an object of credit card and add above details to it 
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard()
            {
                billing_address = billingAddress,
                expire_month = 12,
                expire_year = 2020,
                first_name = "Ross",
                last_name = "McArthur",
                number = "4137350957263509",
                type = "visa"
            };
            */
    }
}
