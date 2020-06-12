using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public int ParentLike { get; set; }
        [ForeignKey("ParentLike")]
        public virtual Parent Parent { get; set; }
        public int PublicationLike { get; set; }
        [ForeignKey("PublicationLike")]
        public virtual Publication Publication { get; set; }
    }
}
