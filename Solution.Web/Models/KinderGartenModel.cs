using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{

    public class KinderGartenModel
    {
        [Key]
        public int KinderGartenId { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [MaxLength(30)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [MaxLength(30)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [Range(0, int.MaxValue)]
        public float Cost { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime DateCreation { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [MaxLength(30)]
        public string Image { get; set; }
        public string nameDir { get; set; }

        [Required(ErrorMessage = "Champs obligatoire")]
        [Range(0, int.MaxValue)]
        public int NbrEmp { get; set; }
        [Display(Name = "Directeur")]
        public int? DirecteurId { get; set; }
    }
}