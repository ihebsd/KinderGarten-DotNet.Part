using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Directeur:User
    {
        public virtual ICollection<KinderGarten> Kindergartens { get; set; }

    }
}
