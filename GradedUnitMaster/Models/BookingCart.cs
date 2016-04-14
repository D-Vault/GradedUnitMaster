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

        public static BookingCart GetCart(HttpContextBase context)
        {
            var cart = new BookingCart();

            cart.CartID = cart.GetCartID(context);

            return cart; 
        }

        public static BookingCart GetCart(Controller Controller)
        {
            return GetCart(Controller.HttpContext);
        }

        public void AddToCart(BookingLineViewModel model)
        {
            var cartItem = db.BookingLine.SingleOrDefault(b => b.cartId == CartID && b.EventId == model.EventId);

            if (cartItem == null)
            {

                var eventDate = db.EventDates.Where(ed => ed.EventId == model.EventId && ed.Date == model.BookingDate)
                    .SingleOrDefault();

                cartItem = new BookingLine()
                {
                    cartId = CartID,
                    EventID = model.EventId,
                    BookingID = model.BookingId,
                    EventBookingDate = eventDate
                };
                db.BookingLine.Add(cartItem);
            }
            db.SaveChanges();
        }


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

        public void EmptyCart()
        {
            var cartItems = db.BookingLine.Where(c => c.cartId == CartID);
            foreach(var cartItem in cartItems)
            {
                db.BookingLine.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<BookingLine> GetCartItems()
        {
            return db.BookingLine.Where(b => b.cartId == CartID).Select(BookingLineViewModel.ViewModel).ToList();
        }

        public int GetCount()
        {
            int? count = db.BookingLine.Where(c=> c.cartId==CartID).Count();

            return count ?? 0;
        }

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

        public int CreateBooking(BookingViewModel model)
        {
            decimal bookingTotal = 0;

            var items = GetCartItems();
            bookingTotal = GetTotal();

            var booking = new Booking()
            {
                BookingDate = DateTime.Now,
                Customer_Account = model.customer_account,
                cost = bookingTotal,
                PaymentMethod = model.paymentMethod
            };

            db.Booking.Add(booking);

            foreach (var item in items)
            {
                db.Booking.Where(b => b.BookingID == booking.BookingID).SingleOrDefault().Bookings.Add(item);
            }

            db.SaveChanges();

            return booking.BookingID;


        }


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