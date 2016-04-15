using System;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    /// <summary>
    /// Displays the details of the location for an event 
    /// </summary>
    public class LocationViewModel
    {

        public int Id { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }

        /// <summary>
        /// Assigns values to the location
        /// </summary>
        public static Expression<Func<Location, LocationViewModel>> ViewModel
        {
            get
            {
                return l => new LocationViewModel()
                {
                    Id = l.LocationID,
                    Street = l.Street,
                    Town = l.Town,
                    Postcode = l.Postcode
                };
            }
        }

    }
}