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
       
        [Key]
        public int DetailID { get; set; }
       
        public string CardNo { get; set; }
                      
        public int ExpireMonth { get; set; }

        public int ExpireYear { get; set; }
       
        public string CardType { get; set; }

        public virtual Account Account { get; set; }

               
        

        public CardDetails()
        {
            CardNo = "0";
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
