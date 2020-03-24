using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
   public  class Claim
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ComplaintId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }
        public int? ParentId { get; set; }//nullable
        // public string recsender { get; set; }
        public string ClaimType { get; set; }
        public string status { get; set; }
        // public string ResourceName { get; set; }//nullable
    }
}
