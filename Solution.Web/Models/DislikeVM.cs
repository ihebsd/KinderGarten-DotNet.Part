using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class DislikeVM
    {
        public int DislikeId { get; set; }
        public int ParentDislike { get; set; }

        public int PublicationDislike { get; set; }
    }
}