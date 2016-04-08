using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    
    public class Event
    {
        /// <summary>
        /// Attributes 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EventID { get; set; }

       
        
        public virtual Business Business { get; set; }

        public decimal EventPrice { get; set; }

       
        public ICollection<EventDates> Dates { get; set; }

      
        public string EventName { get; set; }
     
        [StringLength(500)]
        public string Description { get; set; }
    
        [StringLength(200)]
        public string Restrictions { get; set; }


        public int Capacity { get; set; }

    

        public virtual Location Location { get; set; }

        
       
       

        public virtual Type Type { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public Event()
        {
            EventPrice = 0;
            EventName = " ";
            Description = " ";
            Restrictions = " ";
            Capacity = 0;
            Location = new Location();
            Type = new Type();
            Dates = new HashSet<EventDates>();
        }
    }
}