﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
  
    public  class Claim
    {       
        [System.ComponentModel.DataAnnotations.Key]
        public int ComplaintId { get; set; }
       // public string image { get; set; }
        public string Name { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId ")]
        public virtual User Parent { get; set; }
        // public string recsender { get; set; }
        public string ClaimType { get; set; }
        [DefaultValue("In_progress")]
        public string status { get; set; }
        // public string ResourceName { get; set; }//nullable
    }
}
