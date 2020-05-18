using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class KinderGarten
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
        public DateTime DateCreation { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [MaxLength(30)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Champs obligatoire")]
        [Range(0, int.MaxValue)]
        public int NbrEmp { get; set; }
        public int nbVue { get; set; }
        public string Votes { get; set; }
        public int? DirecteurId { get; set; }
        [ForeignKey("DirecteurId ")]
        public virtual Directeur Directeur { get; set; }
    }
}
