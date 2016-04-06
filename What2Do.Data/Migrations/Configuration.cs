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
            //Execute the following when there are no users in the database
            if (!context.Users.Any())
            {
                try
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

                    Location location = new Location()
                    {
                        Street = business.Street,
                        Town = business.Town,
                        Postcode = business.Postcode
                    };

                    Type type = new Type()
                    {
                        TypeName = "Tests",
                        TypeDescription = "Testing"
                    };



                    Event newEvent = new Event()
                    {
                        Business = business,
                        Capacity = 60,
                        EventName = "Poppins",
                        EventPrice = 0,
                        Description = "Testing123",
                        Type = type,
                        Location = location

                    };
                    context.Events.Add(newEvent);

                    //Passwords for accounts
                    string sPassword = staff.Email;
                    string cPassword = customer.Email;
                    string bPassword = business.Email;


                    //These methods create the new users accounts 
                    CreateStaffUser(context, staff, sPassword);
                    CreateCustomerUser(context, customer, cPassword);
                    CreateBusinessUser(context, business, bPassword);


                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();
                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    );
                }
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

