using GradedUnitMaster.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

namespace GradedUnitMaster.Controllers
{
    public class CheckoutController : MainController
    {
        /// <summary>
        /// Displays a list of payment options 
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPayment()
        {
            string user = this.User.Identity.GetUserId();

            var userFound = db.Accounts.Where(u => u.Id.Equals(user)).SingleOrDefault();

            if (userFound == null)
            {
                ViewBag.Message = "No User found. Please register an account with what2Do, or log in to your account before purchasing bookings";
                RedirectToAction("Info");
            }
           
            return View();
        }

        /// <summary>
        /// Displays the payment displayed
        /// </summary>
        /// <param name="paymentMethod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectPayment(string paymentMethod)
        {
            
            if(paymentMethod.Equals(" ")|| paymentMethod== null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("MakeBooking", new {paymentMethod = paymentMethod });
            }

                
        }

        /// <summary>
        /// gets the inital details of the booking 
        /// </summary>
        /// <param name="paymentMethod">the payment method chosen</param>
        /// <returns>the view and the detail for the preposed booking</returns>
        [HttpGet]
        public ActionResult MakeBooking(string paymentMethod)
        {
            string user = this.User.Identity.GetUserId();

            var userFound = db.Accounts.Where(u => u.Id.Equals(user)).SingleOrDefault();

            BookingViewModel booking = new BookingViewModel
            {
                paymentMethod = paymentMethod,
                customer_account = userFound,
                BookingDate = DateTime.Now.Date
            };

            return View(booking);
        }

        /// <summary>
        /// Gets the details of the booking and begins the booking input process
        /// </summary>
        /// <param name="model">the booking details</param>
        /// <returns>the view to redirect to</returns>
        [HttpPost]
        public ActionResult MakeBooking(BookingViewModel model) 
        {

            var cart = BookingCart.GetCart(this.HttpContext);
            var booking = new Booking();
            if (!model.paymentMethod.Equals("Free Booking"){
                 booking = new Booking()
                {
                    AccountID = getAccount().Id,
                    PaymentMethod = model.paymentMethod,
                    BookingDate = DateTime.Now,
                    cost = cart.GetTotal() + 2
                };

                cart.CreateBooking(booking);
            }
            else
            {
                 booking = new Booking()
                {
                    AccountID = getAccount().Id,
                    PaymentMethod = model.paymentMethod,
                    BookingDate = DateTime.Now,
                    cost = 0
                };

                
            }
            int newBooking = cart.CreateBooking(booking);
            if (model.paymentMethod.Equals("card"))
            { 
                RedirectToAction("CardSelection", "Paypal");
            }
            else if (model.paymentMethod.Equals("paypal"))
            {
                RedirectToAction("PaymentsWithPaypal", new { id = newBooking });
            }

            return View();
        }
    }
}