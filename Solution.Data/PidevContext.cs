

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
        public DbSet<Kid> Kids { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<GeoLocation> GeoLocations{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DirecteurConfiguration());

            modelBuilder.Configurations.Add(new ParentConfiguration());
           
            modelBuilder.Entity<Parent>()
             .HasRequired(s => s.GeoLocation)
             .WithRequiredPrincipal(ad => ad.Parent);

        }
       
        

        public System.Data.Entity.DbSet<Solution.Domain.Entities.Parent> Parents { get; set; }

    }
}

