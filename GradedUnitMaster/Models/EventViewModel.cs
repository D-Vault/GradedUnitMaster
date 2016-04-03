using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class EventViewModel
    {


        public int Id { get; set; }
        public Business Business { get; set; }
        public decimal price { get; set; }
        public ICollection<DateTime> dates { get; set;}
        public string name { get; set; }
        public string description { get; set; }
        public string restrictions { get; set; }
        public int capacity { get; set; }
        public Location location { get; set; }
        public What2Do.Data.Type type { get; set; }

        public static Expression<Func<Event, EventViewModel>> ViewModel
        {
            get
            {
                return e => new EventViewModel()
                {
                    Id = e.EventID,
                    Business = e.Business,
                    price = e.EventPrice,
                    dates = e.EventDates,
                    name = e.EventName,
                    description = e.Description,
                    restrictions = e.Restrictions,
                    capacity = e.Capacity,
                    location = e.Location,
                    type = e.Type
                };
            }
        }

    }
}