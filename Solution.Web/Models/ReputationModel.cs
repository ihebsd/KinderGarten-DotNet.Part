using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class ReputationModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ReputationId { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReputationDate { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
    }
}