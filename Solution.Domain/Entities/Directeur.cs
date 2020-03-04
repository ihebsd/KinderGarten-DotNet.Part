﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Directeur:User
    {
        public int Echelon { get; set; }
        public virtual ICollection<KinderGarten> Kindergartens { get; set; }
        public virtual ICollection<Event> Events { get; set; }


    }
}
