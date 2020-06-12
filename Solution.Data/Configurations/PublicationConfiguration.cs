using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class PublicationConfiguration : EntityTypeConfiguration<Publication>
    {
        public PublicationConfiguration()
        {


            //onetomany 1..1 ->  * Candidat
            HasRequired(pub => pub.Parent).
                WithMany(par => par.Publications).
                HasForeignKey(pub => pub.ParentFk).
                WillCascadeOnDelete(true);

        }

    }
}
