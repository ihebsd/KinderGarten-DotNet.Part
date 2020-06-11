using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class LikeVM
    {
        public int LikeId { get; set; }
        public int ParentLike { get; set; }
       
        public int PublicationLike { get; set; }
    }
}