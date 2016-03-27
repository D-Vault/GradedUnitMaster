using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace What2Do.Data
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [ForeignKey("Business")]
        public int BusinessID { get; set; }

        public float EventPrice { get; set; }

        [Required]
        public ICollection<DateTime> EventDates { get; set; }

        [Required]
        public string EventName { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(200)]
        public string Restrictions { get; set; }

        [Required]
        public int Capacity { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }
        public Location Location { get; set; }

        [Required]
        public int TypeID { get; set; }
    }
}