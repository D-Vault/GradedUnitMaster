using Microsoft.Ajax.Utilities;
using System;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class EventDatesViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public static Expression<Func<EventDates, EventDatesViewModel>> ViewModel
        {
            get
            {
                return d=> new EventDatesViewModel(){
                    Id = d.Id, 
                    Date = d.Date
                };
            }
        }
    }
}