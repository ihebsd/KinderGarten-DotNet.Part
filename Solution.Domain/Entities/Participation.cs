using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Participation
    {
        [Key, Column(Order = 1)]
        public int EventId { get; set; }
        [Key, Column(Order = 2)]
        public int ParentId { get; set; }
        public Event Event { get; set; }
        public Parent Parent { get; set; }
    }
}
