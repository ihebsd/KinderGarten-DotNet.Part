

using Solution.Data.Configurations;
using Solution.Domain.Entities;
using System.Data.Entity;

namespace Solution.Data
{

    public class PidevContext : DbContext
    {
        public PidevContext() : base("kindergarten")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<KinderGarten> KinderGartens { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Reputation> Reputations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DirecteurConfiguration());
           // modelBuilder.Configurations.Add(new ClaimConfiguration());
            modelBuilder.Configurations.Add(new ReputationConfiguration());




        }
    }
}

