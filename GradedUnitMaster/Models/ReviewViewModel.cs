using System;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class ReviewViewModel
    {
        public int ReviewID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        // This attribute is of Type Account due to the possibility 
        // of a business making a review
        public Account Customer_Account { get; set; }

        public Business Business { get; set; }

        public static Expression<Func<Review, ReviewViewModel>>ViewModel
        {
            get
            {
                return r => new ReviewViewModel()
                {
                    ReviewID = r.ReviewID,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Customer_Account = r.Customer_Account,
                    Business = r.Business
                };
            }
        }
    }
}