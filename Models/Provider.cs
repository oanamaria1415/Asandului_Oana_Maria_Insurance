using System.Collections.Generic;

namespace Asandului_Oana_Maria_Insurance.Models
{
    public class Provider
    {
        public int ProviderID { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }

        public ICollection<Policy>? Policies { get; set; }
    }
}
