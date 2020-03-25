using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class ReputationConfiguration : EntityTypeConfiguration<User>
    {
        public ReputationConfiguration()
        {
            HasMany(u => u.Reputations).WithRequired(k => k.Parent).HasForeignKey(p => p.ParentId).WillCascadeOnDelete(true);
        }
    }
}
