using GradedUnitMaster.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GradedUnitMaster.Controllers
{
    /// <summary>
    /// This Class handles the adding, viewing, and cancellation of customer bookings 
    /// </summary>
    public class BookingController : HomeController
    {


        /// <summary>
        /// Displays the list of users current cart items for booking
        /// </summary>
        /// <returns>A list of cart items</returns>
        public ActionResult CardSelection()
        {
            var bookingCart = BookingCart.GetCart(this.HttpContext);

            var viewModel = new BookingCartViewModel
            {
                BookingItems = bookingCart.GetCartItems(),
                CartTotal = bookingCart.GetTotal()
            };

            return View(viewModel);
            
        }





        /// <summary>
        /// Adds new booking to cart
        /// </summary>
        /// <returns>The view with the booking success Message</returns>
        public ActionResult AddBooking(BookingLineViewModel model)
        {
            var eventSelected = db.Events.Single(e => e.EventID == model.EventId);

            var cart = BookingCart.GetCart(this.HttpContext);

            var addedBooking = new BookingLineViewModel()
            {
                EventId = model.EventId,
                BookingDate = model.BookingDate
            };

            cart.AddToCart(addedBooking);

            
            return RedirectToAction("Info");

        }

        /// <summary>
        /// Removes the BookingLine from the booking
        /// </summary>
        /// <param name="model">the bookingline to be removed</param>
        /// <returns>a success message of booking removal</returns>
        [HttpPost]
        public ActionResult RemoveFromCart(BookingLineViewModel model)
        {
            var cart = BookingCart.GetCart(this.HttpContext);

            string EventName = db.Events.FirstOrDefault(item => item.EventID == model.EventId).EventName;

            cart.RemoveFromCart(model.EventId);

            ViewBag.Message = EventName + " has been removed from cart";
            return View("Info");
        }

        /// <summary>
        /// Displays all bookings held by current user
        /// </summary>
        /// <returns>A list of users bookings, if any</returns>
        public ActionResult ViewBookings()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookings = this.db.Booking;

            var usersBookings = from b in bookings
                                where b.Customer_Account.Id.Equals(
                                    User.Identity.GetUserId())
                                select new BookingViewModel()
                                {
                                    Id = b.BookingID,
                                    paymentMethod = b.PaymentMethod, 
                                    BookingDate = b.BookingDate, 
                                    cost=  b.cost, 
                                    customer_account = b.Customer_Account,
                                };
                           
            return View(usersBookings);
        }
        
    }
}