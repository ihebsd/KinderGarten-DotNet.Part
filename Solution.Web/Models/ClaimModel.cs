using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class ClaimModel
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
        [ForeignKey("ParentId ")]
        public virtual User Parent { get; set; }
        public string ClaimType { get; set; }
        [DefaultValue("In_progress")]
        public string status { get; set; }
        // public string ResourceName { get; set; }//nullable
    }
}