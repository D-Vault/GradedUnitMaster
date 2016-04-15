using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using System.Configuration;
using Microsoft.AspNet.Identity;
using What2Do.Data;
using GradedUnitMaster.Models;

namespace GradedUnitMaster.Controllers
{
    public class PaypalController : MainController
    {
        private Payment Payment { get; set; }
       
        /// <summary>
        /// Displays the cards that the user can user, or add another
        /// </summary>
        /// <param name="Booking">The booking to be placed</param>
        /// <returns>Nothing id there is no registered customer, or a list of cards if there is </returns>
        /// 
        public ActionResult Index(int bookingId)
        {
            ViewBag.Booking = bookingId;
            if ((this.IsBusiness() || this.IsCustomer()) && this.getAccount() != null)
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
        public ActionResult PaymentWithCreditCard(int cardId, int bookingId)
        {
            Account account = getAccount();
            
            var bookings = db.BookingLine.Where(b=> b.BookingID == bookingId).ToList();

            var card = db.Cards.SingleOrDefault(c => c.DetailID == cardId);

            var cart = BookingCart.GetCart(this.HttpContext);


            if (card == null)
            {
                return RedirectToAction("AddCard", "Card");
            }
           

            //Now make a list of Item and add the above item to it 
            //you can create as many items as you want and add to this list
            List<Item> Items = new List<Item>();
            foreach(var booking in bookings)
            {
                var eventName = db.Events.SingleOrDefault(e => e.EventID == booking.EventID);

                //create and item for which you are takign payment
                //if you need to add more items in the list
                //then you will need to create multiple item objects or use some loop to instanciate object
                Item item = new Item();
                item.name = eventName.EventName;
                item.currency = "GBP";
                item.price = eventName.EventPrice.ToString();
                item.quantity = "1";
                item.sku = "sku";

                Items.Add(item);
            }
            
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
                expire_month = card.ExpireMonth,
                expire_year = card.ExpireYear,
                first_name = account.FirstName,
                last_name = account.Surname,
                number = card.CardNo.ToString(),
                type = card.CardType
            };

            //Specify details of your payment ammount
            Details details = new Details()
            {
                fee = "2",
                subtotal = (cart.GetTotal() + 2).ToString(),
                tax = "0"
            };

            Amount amnt = new Amount()
            {
                currency = "GBP",
                total = (cart.GetTotal() + 2).ToString(),
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

                APIContext apiContext = PaypalConfig.GetAPIContext();

                //Create is a payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we recived above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successful else not
                if (createdPayment.state.ToLower() != "approved")
                {
                    string message = "Hi there " + account.FirstName + "! Your booking has been made. Please go to the website to get info " +
                        "on your bookings";
                    this.sendMessage(message, account.MobileNo);

                    return View("SuccessView");
                    
                }

            }
            catch (PayPal.PayPalException ex)
            {
                return View("FailureView");
            }

            return View("SuccessView"); }


    
        /// <summary>
        /// Executes the paypal method of payment 
        /// </summary>
        /// <returns>The resultant view of payment success, or failure</returns>
    public ActionResult PaymentWithPaypal(int id)
    {
        APIContext apiContext = PaypalConfig.GetAPIContext();
        try
        {
            string payerId = Request.Params["PayerID"];

            if (string.IsNullOrEmpty(payerId))
            {
                string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                    "/Home/PaymentWithPaypal?";


                var guid = Convert.ToString((new Random().Next(100000)));

                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, id);

                var links = createdPayment.links.GetEnumerator();

                string paypalRedirectUrl = null;

                while (links.MoveNext())
                {
                    Links lnk = links.Current;

                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }

                Session.Add(guid, createdPayment.id);

                return Redirect(paypalRedirectUrl);
            }
            else
            {

                var guid = Request.Params["guid"];

                var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                if (executedPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }

            }
        }
        catch (Exception e)
        {
            return View("FailureView");
        }

        return View("SuccessView");
    }

        /// <summary>
        /// Performs the payment 
        /// </summary>
        /// <param name="apiContext">The details for paypal API</param>
        /// <param name="payerID">The Customer's ID</param>
        /// <param name="paymentId">the Uniquiue payment ID</param>
        /// <returns></returns>
    private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentId)
    {
        var PaymentExecution = new PaymentExecution() { payer_id = payerID };
        this.Payment = new Payment() { id = paymentId };
        return this.Payment.Execute(apiContext, PaymentExecution);
    }

    private Payment CreatePayment(APIContext apiContext, string redirectURL, int booking)
    {
            var bookings = db.BookingLine.Where(b => b.BookingID == booking);

            var cart = BookingCart.GetCart(this.HttpContext);

        var itemList = new ItemList() { items = new List<Item>() };
            foreach (var singleBooking in bookings)
            {
                var bookingEvent = db.Events.SingleOrDefault(e => e.EventID == singleBooking.EventID);
                itemList.items.Add(new Item()
                {
                    name = bookingEvent.EventName,
                    currency = "GBP",
                    price = bookingEvent.EventPrice.ToString(),
                    quantity = "1",
                    sku = "sku"
                });
            }

        var payer = new Payer()
        {

            payment_method = "paypal"

        };

        var redirUrls = new RedirectUrls()
        {
            cancel_url = redirectURL,
            return_url = redirectURL
        };

            var details = new Details()
            {
                fee = "2",
                shipping = "0",
                subtotal = cart.GetTotal().ToString()
            };

        var ammount = new Amount()
        {
            currency = "GBP",
            total = (cart.GetTotal()+2).ToString(),
            details = details
        };

        var TransactionList = new List<Transaction>();
        Random rdm = new Random();
        TransactionList.Add(new Transaction()
        {
            description = "Transaction Description",
            invoice_number = Convert.ToString(rdm.Next(1, 20)),
            amount = ammount,
            item_list = itemList
        });

        this.Payment = new Payment()
        {

            intent = "sale",
            payer = payer,
            transactions = TransactionList,
            redirect_urls = redirUrls
        };

        return this.Payment.Create(apiContext);


    }

}
}