using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoiningDataLab
{
    class Order
    {
        public int CustomerID { get; set; }
        public int OrderID { get; set; } // We never use this, but it'd probably be used for corresponding item to a unique value like customerName/customerID.
        public string? Item { get; set; } // We use ? to indicate we don't care if something is null.
        public decimal? Price { get; set; } // CustomerID and OrderID are important, so they aren't allowed to be null.
        public int? Quantity { get; set; }
    }
}