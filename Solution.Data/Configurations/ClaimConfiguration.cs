using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
   public class ClaimConfiguration : EntityTypeConfiguration<User>
    {
        public ClaimConfiguration()
        {
          //  HasMany(u => u.claims).WithRequired(k => k.Parent).HasForeignKey(p => p.ParentId).WillCascadeOnDelete(true);
        }
    }
}
