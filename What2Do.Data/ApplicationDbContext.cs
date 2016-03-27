using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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
        public ApplicationDbContext()
            : base  ("What2Do", throwIfV1Schema: false)
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
