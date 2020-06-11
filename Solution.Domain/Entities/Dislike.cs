using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Dislike
    {
        public int DislikeId { get; set; }
        public int ParentDislike { get; set; }
        [ForeignKey("ParentDislike")]
        public virtual Parent Parent { get; set; }
        public int PublicationDislike { get; set; }
        [ForeignKey("PublicationDislike")]
        public virtual Publication Publication { get; set; }
    }
}
