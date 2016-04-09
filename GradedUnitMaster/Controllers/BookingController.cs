using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

            }

            return View();
        }
        
    }
}