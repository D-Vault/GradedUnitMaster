namespace What2Do.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
           
        }

        protected override void Seed(ApplicationDbContext context)
        {
            

        }
           
       

        //Creates an admin with the required criteria 
        private void CreateStaffUser(ApplicationDbContext context, string adminEmail, string adminUsername, string adminName, string adminPassword,
            string town, string postcode, string TelNo, string MobileNo, string Street)
        {
            //Creates a new admin Object with the values of the parameters 
            var adminUser = new Staff
            {
                UserName = adminUsername,
                Email = adminEmail,
                Name = adminName,
                StaffRole = "Events Organiser", 
                Town = town, 
                Street = Street, 
                MobileNo = MobileNo, 
                TelNo = TelNo, 
                Postcode = postcode, 
                                
            };
           
            //Creates and intiailises the componients of adding the admin
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
            var userCreateResult = userManager.Create(adminUser, adminPassword);

            //If the creation of the admin then execute the following 
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }
        }
    }
    }

