using Asandului_Oana_Maria_Insurance.Models;
using Microsoft.EntityFrameworkCore;


namespace Asandului_Oana_Maria_Insurance.Data
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Provider> Provider { get; set; } = default!;
        public DbSet<Policy> Policy { get; set; } = default!;
        public DbSet<Claim> Claim { get; set; } = default!;
        public DbSet<Payment> Payment { get; set; } = default!;
    }
}
