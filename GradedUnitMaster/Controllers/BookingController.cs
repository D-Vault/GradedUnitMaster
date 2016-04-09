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
        /// 
        /// </summary>
        /// <returns>The view with the booking success Message</returns>
        public ActionResult AddBooking()
        {


            return Redirect("Info");

        }


        public ActionResult ViewBookings()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookings = this.db.Bookings;

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
                           
            return View();
        }
        
    }
}