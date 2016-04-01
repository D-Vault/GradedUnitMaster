using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using System.Configuration;
using Microsoft.AspNet.Identity;
using What2Do.Data;

namespace GradedUnitMaster.Controllers
{ 
    public class PaypalController : MainController
    {
        private Payment Payment { get; set; }

        // GET: Paypal
        public ActionResult Index(int BookingId    )
        {
            
            if (this.IsBusiness() || this.IsCustomer() || this.getAccount() !=null)
            {
                Account a = getAccount();
                ICollection<CardDetails> cards = a.Cards;
                return View(cards.ToList());
            }
            else
            {
                RedirectToAction("Index", "Home");
            }

            return View();
        }

      
        
        /// <summary>
        /// Method executes the process of paying for a booking by card
        /// </summary>
        /// <returns>Result of the process</returns>
        public ActionResult PaymentWithCreditCard(int CardID)
        {
            Account account = getAccount();

            CardDetails card = getAccount().Cards.Where(c=> c.DetailID == CardID).FirstOrDefault();
            
            //create and item for which you are takign payment
            //if you need to add more items in the list
            //then you will need to create multiple item objects or use some loop to instanciate object
            Item item = new Item();
            item.name = "New Item";
            item.currency = "GBP";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";

            //Now make a list of Item and add the above item to it 
            //you can create as many items as you want and add to this list
            List<Item> Items = new List<Item>();
            Items.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = Items;

            //Address for the payment 
            Address billingAddress = new Address()
            {
                city = account.Town,
                country_code = "GB",
                line1 = account.Street,
                postal_code = account.Postcode

            };

           // Now create an object of credit card and add above details to it 
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard()
            {
                billing_address = billingAddress,
                expire_month = 12,
                expire_year = 2020,
                first_name= account.Name,
                last_name = account.Name,
                number = "4137350957263509",
                type = "visa"
            };

            //Specify details of your payment ammount
            Details details = new Details()
            {
                shipping = "1",
                subtotal = "5",
                tax = "1"
            };

            Amount amnt = new Amount()
            {
                currency = "GBP",
                total = "7",
                details = details
            };
            Random rdm = new Random();
            Transaction tran = new Transaction()
            {
                amount = amnt,
                description = "Description of the payment ammount.",
                item_list = itemList,
                invoice_number = Convert.ToString(rdm.NextDouble() * 100)
            };



            //Now, we have to make a list of transaction and the transaction object
            //for credit card payments, set the CreditCard which we made above

            List<Transaction> transation = new List<Transaction>();
            transation.Add(tran);

            //Now we need to specify the funding instrument of the payer 
            //for credit card payments, set the credit card which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;


            //Payment creation API requires a list of FundingInstrument
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            //Now create payer object and assign the funding instrument to the list object
            Payer payr = new Payer()
            {
                funding_instruments = fundingInstrumentList,
                payment_method = "credit_card"
            };

            Payment pymnt = new Payment()
            {
                intent = "sale",
                payer = payr,
                transactions = transation
            };

            try
            {
                //getting context from the paypal 
                //basically we are sending the clientID and clientSecret Key in this function 
                //to get the context from the paypal API to make the payment. 

                //apiContext object has a accesstoken which is sent by the paypal
                //to athenticate the p  ayment to facillitator account. 
                //An access token could be an alphanumeric string 

               APIContext apiContext = System.Configuration.Configuration.GetAPIContext();

                //Create is a payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we recived above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successful else not
                if (createdPayment.state.ToLower() != "approved")
               {
                    return View("SuccessView");
               }

            }
            catch (PayPal.PayPalException ex)
            {
                return View("FailureView");
            }
            
            return View("SuccessView");}


    }
    
}