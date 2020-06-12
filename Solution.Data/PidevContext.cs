

using Solution.Data.Configurations;
using Solution.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Solution.Data
{

    public class PidevContext : DbContext
    {
        public PidevContext() : base("kindergarten")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<KinderGarten> KinderGartens { get; set; }
        public DbSet<VoteLog> VoteModels { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<FeedBack> Reputations { get; set; }
        public DbSet<AdminNotif> Anotifs { get; set; }
        public DbSet<Kid> Kids { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }



        public DbSet<Event> Events { get; set; }
        public DbSet<Participation> Participation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new DirecteurConfiguration());

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Entity<Parent>().HasMany(p => p.Dislikes).WithRequired(d => d.Parent).HasForeignKey(d => d.ParentDislike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Parent>().HasMany(p => p.Likes).WithRequired(d => d.Parent).HasForeignKey(d => d.ParentLike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Publication>().HasMany(p => p.Dislikes).WithRequired(d => d.Publication).HasForeignKey(d => d.PublicationDislike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Publication>().HasMany(p => p.Likes).WithRequired(d => d.Publication).HasForeignKey(d => d.PublicationLike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Parent>().HasMany(par => par.CarPools).WithOptional(car => car.Parent)
                      .HasForeignKey(car => car.idParent)
                      .WillCascadeOnDelete(false);
            modelBuilder.Entity<Parent>()
             .HasRequired(s => s.GeoLocation)
             .WithRequiredPrincipal(ad => ad.Parent);

        }
    }
}

