using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class FeedBack
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int FeedBackId { get; set; }        
        [DataType(DataType.Date)]
        public DateTime FeedBackDate { get; set; }
        public string Description  { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId ")]
        public virtual User Parent { get; set; }

    }
}
