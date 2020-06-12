using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class FeedBackConfiguration : EntityTypeConfiguration<User>
    {
        public FeedBackConfiguration()
        {
            HasMany(u => u.FeedBacks).WithRequired(k => k.Parent).HasForeignKey(p => p.ParentId).WillCascadeOnDelete(true);
        }
    }
}
