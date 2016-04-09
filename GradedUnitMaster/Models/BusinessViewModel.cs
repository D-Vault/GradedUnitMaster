using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    /// <summary>
    /// This class is the medium for viewing Business info on the system
    /// </summary>
    public class BusinessViewModel
    {
        /// <summary>
        /// Attributes
        /// </summary>
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }       
        public string Postcode { get; set; }
        public string TelNo { get; set; }
        public ICollection<ReviewViewModel>Reviews { get; set; }

        public static Expression<Func<Business, BusinessViewModel>>ViewModel
        {
            get
            {
                return b => new BusinessViewModel()
                {
                    BusinessID = b.BusinessID,
                    BusinessName = b.BusinessName,
                    Street = b.Street,
                    Town = b.Town,
                    Postcode = b.Postcode,
                    TelNo = b.TelNo,
                    Reviews = b.Review.AsQueryable().Select(ReviewViewModel.ViewModel).ToList()
                };
            }
        }
    }
}