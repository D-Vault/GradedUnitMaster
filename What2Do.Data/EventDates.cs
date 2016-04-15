using System;
using System.ComponentModel.DataAnnotations;

namespace What2Do.Data
{
    /// <summary>
    /// Table that contains the details of a specific event date
    /// </summary>
    public class EventDates
    {
        /// <summary>
        /// Attributes 
        /// </summary>
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int bookings { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}