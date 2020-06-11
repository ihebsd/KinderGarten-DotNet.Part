

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

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DirecteurConfiguration());
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Entity<Parent>().HasMany(p => p.Dislikes).WithRequired(d => d.Parent).HasForeignKey(d => d.ParentDislike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Parent>().HasMany(p => p.Likes).WithRequired(d => d.Parent).HasForeignKey(d => d.ParentLike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Publication>().HasMany(p => p.Dislikes).WithRequired(d => d.Publication).HasForeignKey(d => d.PublicationDislike).WillCascadeOnDelete(false);
            modelBuilder.Entity<Publication>().HasMany(p => p.Likes).WithRequired(d => d.Publication).HasForeignKey(d => d.PublicationLike).WillCascadeOnDelete(false);


        }
    }
}

