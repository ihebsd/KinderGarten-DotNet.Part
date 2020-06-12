using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class FeedBackModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int FeedBackId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FeedBackDate { get; set; }
        public string sentiment { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
    }
}