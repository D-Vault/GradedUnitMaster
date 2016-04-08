using System;
using System.ComponentModel.DataAnnotations;

namespace What2Do.Data
{
    public class EventDates
    {

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}