using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IDbSet<Account> Account { get; set; }
        public IDbSet<CardDetails>Cards { get; set; }
        public IDbSet<Event>Events { get; set; }
        public IDbSet<Booking> Bookings { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Type>Types { get; set; }


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
    }
}
