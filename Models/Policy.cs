using Asandului_Oana_Maria_Insurance.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Asandului_Oana_Maria_Insurance.Models
{
    public class Policy
    {
        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Premium { get; set; }

       
        public int? CustomerID { get; set; }
        public int? ProviderID { get; set; }

        
        public Customer? Customer { get; set; }
        public Provider? Provider { get; set; }

        public ICollection<Claim>? Claims { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
