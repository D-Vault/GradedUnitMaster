using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    
    public class Event
    {
       
       
        public int EventID { get; set; }

       
        public int BusinessID { get; set; }

        public decimal EventPrice { get; set; }

       
        public virtual ICollection<DateTime> EventDates { get; set; }

      
        public string EventName { get; set; }
     
        [StringLength(500)]
        public string Description { get; set; }
    
        [StringLength(200)]
        public string Restrictions { get; set; }


        public int Capacity { get; set; }

        
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

        
        
        public int TypeID { get; set; }

        public virtual Type Type { get; set; }

        public Event()
        {
            EventPrice = 0;
            EventName = " ";
            Description = " ";
            Restrictions = " ";
            Capacity = 0;
            Location = new Location();
            Type = new Type();
        }
    }
}