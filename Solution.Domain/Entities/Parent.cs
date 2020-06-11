using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Parent : User
    {
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }


    }
}
