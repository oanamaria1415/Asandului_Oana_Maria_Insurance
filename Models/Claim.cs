using Asandului_Oana_Maria_Insurance.Models;
using System;

namespace Asandului_Oana_Maria_Insurance.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public DateTime ClaimDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public int? PolicyID { get; set; }
        public Policy? Policy { get; set; }
    }
}
