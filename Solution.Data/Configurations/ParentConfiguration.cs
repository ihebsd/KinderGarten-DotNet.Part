using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
   public  class ParentConfiguration : EntityTypeConfiguration <Parent>
    {
        public ParentConfiguration()
        {
            HasMany(par => par.CarPools).WithOptional(car => car.Parent)
           .HasForeignKey(car => car.idParent)
           .WillCascadeOnDelete(false);
           
        }
    }
}
