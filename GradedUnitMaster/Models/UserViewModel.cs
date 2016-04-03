using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class UserViewModel
    {

        public string Id { get; set; }

        public string FirstName { get; set;}

        public string Surname { get; set; }

        

        public static Expression<Func<Account, UserViewModel>>ViewModel
        {
            get
            {
                return u => new UserViewModel()
                {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        Surname = u.Surname
                };
            }
        }   
    }
}