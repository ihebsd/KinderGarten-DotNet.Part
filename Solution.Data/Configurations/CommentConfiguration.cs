using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {


            //onetomany 1..1 ->  * Candidat


            HasRequired(com => com.Publication).
                WithMany(pub => pub.Comments).
                HasForeignKey(com => com.PublicationFK).
                WillCascadeOnDelete(true);



        }
    }
}
