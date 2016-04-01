using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace What2Do.Data
{

    /// <summary>
    /// Connects to the database source
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Tables in the Database
        /// </summary>
        public IDbSet<Staff> Staff { get; set;}
      
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Booking> Bookings { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Type> Types { get; set; }
        public IDbSet<Business>Business { get; set; }
        public IDbSet<Customer>Customers { get; set; }
        public IDbSet<BookingLine>BookingLine {get;set;}


        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApplicationDbContext()
            : base  ("Connection", throwIfV1Schema: false)
        {
        } 

        /// <summary>
        /// Creates a new database for the system
        /// </summary>
        /// <returns>New Database</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<What2Do.Data.CardDetails> CardDetails { get; set; }
    }
}
