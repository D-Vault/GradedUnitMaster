
using System.Linq;

using System.Web.Mvc;
using Twilio;
using What2Do.Data;

using Microsoft.AspNet.Identity;

namespace GradedUnitMaster.Controllers
{
    /// <summary>
    /// Is the root controller for the system, supplying
    /// general operations to sub-classes
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// Attributes
        /// </summary>
        protected ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Checks to see if a staff member is logged in 
        /// </summary>
        /// <returns>The outcome of checking that the 
        /// Id matches an existing staff member</returns>
        public bool IsStaff()
        {
            var user = this.User.Identity.GetUserId();

            bool isStaff = (user != null && db.Users.OfType<Staff>()
                .Where(s => s.Id == user).Any());

            return isStaff;
        }


        /// <summary>
        /// Checks to see if a business account has logged in
        /// </summary>
        /// <returns>the outcome of checking that the
        /// id matches an existing business
        /// </returns>
        public bool IsBusiness()
        {
            var user = this.User.Identity.GetUserId();

            bool isBusiness = (user != null && db.Users.OfType<Business>()
                .Where(b => b.Id == user).Any());

            return isBusiness;
        }

        /// <summary>
        /// Checks to see if a customer account has logged in
        /// </summary>
        /// <returns>the outcome of checking that the id 
        /// matches an existing customer</returns>
        public bool IsCustomer()
        {

            var user = this.User.Identity.GetUserId();

            bool isCustomer = (user != null && db.Users.OfType<Customer>()
                .Where(c => c.Id == user).Any());

            return isCustomer;


        }

        /// <summary>
        /// Method sends a message to the user
        /// </summary>
        public void sendMessage(Models.Message message)
        {
            var Twilio = new TwilioRestClient(
           System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
           System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);

            Twilio.SendMessage(
            System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"], message.recepient, message.body);
        }


        /// <summary>
        /// Finds current user logged into the system
        /// </summary>
        /// <returns>the user found, otherwise null</returns>
        public What2Do.Data.Account getAccount()
        {
            var user = this.User.Identity.GetUserId();

            What2Do.Data.Account foundAccount = db.Users.OfType<What2Do.Data.Account>()
                                                    .Where(u => u.Id == user).FirstOrDefault();

            if (foundAccount != null) return foundAccount;
            else return null;
    

    }

    }
}

