using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class EventViewModel
    {


        public int Id { get; set; }
        public virtual Business Business { get; set; }
        public decimal price { get; set; }
        public ICollection<EventDatesViewModel> dates { get; set;}
        public string name { get; set; }
        public string description { get; set; }
        public string restrictions { get; set; }
        public int capacity { get; set; }
        public virtual Location location { get; set; }
        public virtual What2Do.Data.Type type { get; set; }

        public static Expression<Func<Event, EventViewModel>> ViewModel
        {
            get
            {
                return e => new EventViewModel()
                {
                    Id = e.EventID,
                    Business = e.Business,
                    price = e.EventPrice,
                    dates = e.Dates.AsQueryable().Select(EventDatesViewModel.ViewModel).ToList(),
                    name = e.EventName,
                    description = e.Description,
                    restrictions = e.Restrictions,
                    capacity = e.Capacity,
                    
                };
            }
        }

    }

    /// <summary>
    /// Handles the input details of the event
    /// </summary>
    public class EventInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public Business Business { get; set; }

     
        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "Event Price is Required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "At least one Event Date is Required")]
        public ICollection<EventDatesInputModel> dates { get; set; }

        [Required(ErrorMessage = "Event Name is Required")]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Event Description")]
        public string Description { get; set; }

        
        [Display(Name = "Event Restrictions")]
        public string Restrictions { get; set; }

        [Required]
        [Display(Name = "Event Capacity")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "An event Location is Required")]
        [Display(Name = "Event Location")]
        public Location Location { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public What2Do.Data.Type Type { get; set; }
    }

    
}