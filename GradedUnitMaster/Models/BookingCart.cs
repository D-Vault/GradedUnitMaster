using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using What2Do.Data;

namespace GradedUnitMaster.Models
{

    /// <summary>
    /// Acts as the tempoary storage of users bookings with the system
    /// </summary>
    public partial class BookingCart
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string CartID { get; set; }
        public const string CartSessionkey = "CartID";

        /// <summary>
        /// Gets the current cart contents
        /// </summary>
        /// <param name="context">the current session</param>
        /// <returns>booking cart details</returns>
        public static BookingCart GetCart(HttpContextBase context)
        {
            var cart = new BookingCart();

            cart.CartID = cart.GetCartId(context);

            return cart; 
        }

        /// <summary>
        /// Gets the current cart contents
        /// </summary>
        /// <param name="Controller">The current controller</param>
        /// <returns>Booking cart details</returns>
        public static BookingCart GetCart(Controller Controller)
        {
            return GetCart(Controller.HttpContext);
        }

        /// <summary>
        /// Adds a new booking line to the cart
        /// </summary>
        /// <param name="model">The booking line to be added </param>
        public void AddToCart(BookingLineViewModel model)
        {
            var cartItem = db.BookingLine.SingleOrDefault(b => b.cartId == CartID && b.EventID == model.EventId);

            if (cartItem == null)
            {

                var eventDate = db.EventDates.Where(ed => ed.EventId == model.EventId && ed.Id == model.BookingDate)
                    .SingleOrDefault();

                cartItem = new BookingLine()
                {
                    cartId = CartID,
                    EventID = model.EventId,
                    EventDateId = model.BookingDate,

                };
                db.BookingLine.Add(cartItem);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the booking line from cart
        /// </summary>
        /// <param name="id">The booking line unique ID</param>
        public void RemoveFromCart(int id)
        {
            var cartItem = db.BookingLine.SingleOrDefault(b => b.cartId == CartID && b.EventID == id);

            if (cartItem != null)
            {
                db.BookingLine.Remove(cartItem);
            }
            else
            {
                return;
            }

            db.SaveChanges();
        }

        /// <summary>
        /// Empty's all contents from cart 
        /// </summary>
        public void EmptyCart()
        {
            var cartItems = db.BookingLine.Where(c => c.cartId == CartID);
            foreach(var cartItem in cartItems)
            {
                db.BookingLine.Remove(cartItem);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Gets all booking lines in the cart 
        /// </summary>
        /// <returns>The list of booking lines</returns>
        public List<BookingLine> GetCartItems()
        {
            return db.BookingLine.Where(b => b.cartId == CartID).ToList();
        }

        /// <summary>
        /// Gets the total ammount of booking lines in cart 
        /// </summary>
        /// <returns>the count of booking lines in cart</returns>
        public int GetCount()
        {
            int? count = db.BookingLine.Where(c=> c.cartId==CartID).Count();

            return count ?? 0;
        }

        /// <summary>
        /// gets the net sum for the cart 
        /// </summary>
        /// <returns>the total ammount calculated</returns>
        public decimal GetTotal()
        {
            decimal? total= 0 ;

            var bookings = db.BookingLine.Where(c => c.cartId == CartID).ToList();
            
            foreach(var booking in bookings)
            {
                total += db.Events.Where(b => b.EventID == booking.EventID).SingleOrDefault().EventPrice;
            }

            return total ?? decimal.Zero;
        }

        /// <summary>
        /// Creates a new booking 
        /// </summary>
        /// <param name="booking">the booking to be added</param>
        /// <returns>the booking Id of the new booking</returns>
        public int CreateBooking(Booking booking)
        {
            decimal bookingTotal = 0;

            var items = GetCartItems();
            bookingTotal = GetTotal();


            db.Booking.Add(booking);

            foreach (var item in items)
            {
                db.Booking.Where(b => b.BookingID == booking.BookingID).SingleOrDefault().Bookings.Add(item);
            }

            db.SaveChanges();

            return booking.BookingID;


        }

        /// <summary>
        /// Gets the cart Id if one exists, 
        /// otherwise it creates a new one 
        /// </summary>
        /// <param name="context">the current session</param>
        /// <returns>the carts' ID</returns>
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartID] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartID] = context.User.Identity.Name;
                }

                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartID] = tempCartId.ToString();
                }
            }

            return context.Session[CartID].ToString();
        }


        /// <summary>
        /// Moves the cart contents to a new cart 
        /// </summary>
        /// <param name="userName"></param>
        public void MigrateCart(string userName)
        {
            var cart = db.BookingLine.Where(b => b.cartId == CartID);
            foreach(var item in cart)
            {
                item.cartId = userName;
            }

            db.SaveChanges();
        }
    }


}