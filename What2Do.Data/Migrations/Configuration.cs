namespace What2Do.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        /// <summary>
        /// Manages the creation and intergration of a new database structure
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
           
        }

        /// <summary>
        /// Adds entries into the database upon its creation
        /// </summary>
        /// <param name="context">The Database context</param>
        protected override void Seed(ApplicationDbContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();
            //Execute the following when there are no users in the database
            if (!context.Users.Any())
            {

                

                    //Creates and assigns variables that will make up the Staff account 

                    Staff staff = new Staff
                    {
                        Email = "admin@admin.com",
                        UserName = "admin@admin.com",
                        FirstName = "Staff",
                        Surname = "Member",
                        Town = "Greenock",
                        Street = "123 fakestreet",
                        TelNo = "01234567891",
                        MobileNo = "01234567891",
                        Postcode = "PA15 2AE",
                        EmailConfirmed = true
                    };


                    //Creates and assigns variables that will make up the Customer account 
                    Customer customer = new Customer
                    {
                        Email = "customer@customer.com",
                        UserName = "customer@customer.com",
                        FirstName = "Customer",
                        Surname = "-chan",
                        Street = "123 fakestreet",
                        Town = "Downtown",
                        TelNo = "12345987001",
                        MobileNo = "12345987001",
                        Postcode = "PP123",
                        EmailConfirmed = true
                    };

                    //Creates and assigns variables that will make up the Business account 
                    Business business = new Business
                    {
                        Email = "business@Business.com",
                        UserName = "Business",
                        FirstName = "Business",
                        Surname = "",
                        Street = "123 fakestreet",
                        Town = "Memetown",
                        TelNo = "78945612302",
                        MobileNo = "32165498702",
                        Postcode = "PP123",
                        BusinessName = "Business",
                        EmailConfirmed = true

                    };
                               
                    //Passwords for accounts
                    string sPassword = staff.Email;
                    string cPassword = customer.Email;
                    string bPassword = business.Email+"1";

                    //These methods create the new users accounts 
                    CreateStaffUser(context, staff, sPassword);
                    CreateCustomerUser(context, customer, cPassword);
                    CreateBusinessUser(context, business, bPassword);


                    //Creating new Location
                    Location location = new Location()
                    {
                        Street = business.Street,
                        Town = business.Town,
                        Postcode = business.Postcode, 
                        Business = business
                    };

                    //Creating new Type
                    Type type = new Type()
                    {
                        TypeName = "Tests",
                        TypeDescription = "Testing"
                    };


                    //Creates new event
                    Event newEvent = new Event()
                    {
                        Business = business,
                        Capacity = 60,
                        EventName = "Poppins",
                        EventPrice = 0,
                        Description = "Testing123",
                        Type = type,
                        Location = location,
                        
                    };


                //Adds event dates to the event
                newEvent.Dates.Add(new EventDates()
                    {
                        Date= DateTime.Now.AddDays(3),
                        bookings = 60
                    });
                    
                    //Adds event dates to the event
                    newEvent.Dates.Add(new EventDates()
                    {
                        Date = DateTime.Now.AddDays(6),
                        bookings = 30,
                        Event = newEvent
                    });

                //Create new review
                Review review = new Review()
                    {
                        Customer_Account = customer,
                        Rating = 5,
                        Comment = "Good Show so it was", 
                        Business = business
                    };

                //Create new review
                Review review2 = new Review()
                {
                    Customer_Account = customer,
                    Rating = 4,
                    Comment = "Good Show so it was",
                    Business = business
                };

               
                //Adds a new Booking to hold a collection of booking lines
                Booking booking = new Booking()
                {
                    BookingDate = DateTime.Now,
                    cost = 0, 
                    Customer_Account = customer, 
                    PaymentMethod = "Free Booking"
                };

                //Adds new booking for specific event 
                BookingLine book1 = new BookingLine()
                {
                    Event = newEvent,
                    EventBookingDate = newEvent.Dates
                    .Where(e => e.Date.Equals(DateTime.Now.AddDays(6))).SingleOrDefault(),
                    Booking = booking
                };



                //Adds new entries to database
                context.Booking.AddOrUpdate(booking);
                context.BookingLine.AddOrUpdate(book1);
                context.Reviews.AddOrUpdate(review2);
                context.Reviews.AddOrUpdate(review);
                context.Locations.AddOrUpdate(location);
                context.Types.AddOrUpdate(type);
                context.Events.AddOrUpdate(newEvent);
                

            }
        }

       

        /// <summary>
        /// Creates a new Business entry into the database
        /// </summary>
        /// <param name="context">The database to add it to</param>
        /// <param name="business">The Business to be added</param>
        /// <param name="Password">The Password for the account</param>
        private void CreateBusinessUser(ApplicationDbContext context, Business business, string Password)
        {
            //Creates and intiailises the componients of adding the business
            var userStore = new UserStore<Business>(context);
            var userManager = new UserManager<Business>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireDigit = false
            };
            //Adds the admin to the database 
            var userCreateResult = userManager.Create(business, Password);

            //If the creation of the Business has failed throw exception
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }
        }



        /// <summary>
        /// Creates a new Business entry into the database
        /// </summary>
        /// <param name="context">The database to add the entry to</param>
        /// <param name="staff">The account to add</param>
        /// <param name="Password">The password for the account</param>
        private void CreateStaffUser(ApplicationDbContext context, Staff staff, string Password)
        {
           
           //Creates and intiailises the componients of adding the staff
            var userStore = new UserStore<Staff>(context);
            var userManager = new UserManager<Staff>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireDigit = false
            };
            //Adds the admin to the database 
            var userCreateResult = userManager.Create(staff, Password);

            //If the creation of the Staff has failed throw exception
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }
        }


      
        private void CreateCustomerUser(ApplicationDbContext context, Customer customer, string Password)
        { 

            //Creates and intiailises the componients of adding the customer
            var userStore = new UserStore<Customer>(context);
            var userManager = new UserManager<Customer>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireDigit = false
            };
            //Adds the customer to the database 
            var userCreateResult = userManager.Create(customer, Password);

            //If the creation of the customer has failed throw exception
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }
        }
    }
    }

