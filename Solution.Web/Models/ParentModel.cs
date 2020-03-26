using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class ParentModel : User
    {
        public virtual ICollection<CarPool> CarPools { get; set; }
        public virtual ICollection<Kid> Kids { get; set; }
    }
}