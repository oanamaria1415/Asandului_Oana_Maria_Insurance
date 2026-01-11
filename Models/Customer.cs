using System;
using System.Collections.Generic;

namespace Asandului_Oana_Maria_Insurance.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }  
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Policy>? Policies { get; set; }
    }
}
