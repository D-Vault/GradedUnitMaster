using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Do.Data
{
    public class Customer:Account
    {
        public int CustomerID { get; set; }

        public ICollection<Booking>Booking { get; set; }

    }
}
