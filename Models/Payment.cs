using Asandului_Oana_Maria_Insurance.Models;
using System;

namespace Asandului_Oana_Maria_Insurance.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? Method { get; set; }

        public int? PolicyID { get; set; }
        public Policy? Policy { get; set; }
    }
}
