using Microsoft.Ajax.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class EventDatesViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Bookings { get; set; }

        public static Expression<Func<EventDates, EventDatesViewModel>> ViewModel
        {
            get
            {
                return d=> new EventDatesViewModel(){
                    Id = d.Id, 
                    Date = d.Date,
                    Bookings = d.bookings
                };
            }
        }
    }

    public class EventDatesInputModel
    {
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [Required]
        public int Event { get; set; }
                
        }
}