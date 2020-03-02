using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
   public class DirecteurConfiguration: EntityTypeConfiguration<Directeur>
    {
        public DirecteurConfiguration()
        {
            HasMany(u => u.Kindergartens).WithRequired(k => k.Directeur).HasForeignKey(p => p.DirecteurId).WillCascadeOnDelete(true);

        }
    }
}
