using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class VoteLog
    {
        [Key]
        public int AutoId { get; set; }
        public Int16 SectionId { get; set; }
        public int VoteForId { get; set; }
        public string UserName { get; set; }
        public Int16 Vote { get; set; }
        public bool Active { get; set; }


    }
}
