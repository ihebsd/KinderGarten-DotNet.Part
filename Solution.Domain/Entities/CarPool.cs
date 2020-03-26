using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class CarPool
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public String Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "From is required")]
        public string From { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "To is required")]
        public string To { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Time is required")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message is required")]
        public string Message { get; set; }
        /* [Required]
         [DataType(DataType.Date)]
         public DateTime weekly { get; set; }
          [Required]
          [DataType(DataType.Date)]
          public DateTime Daily { get; set; }
          [Required]
          [DataType(DataType.Time)]
          public DateTime  EveryWeekDay { get; set; }
          [Required]
          [DataType(DataType.Date)]
          public DateTime weekly { get; set; }
          [Required]
          [DataType(DataType.Date)]
          public DateTime Until { get; set; }*/
        public int? idParent { get; set; }
        [ForeignKey("idParent")]
        public virtual Parent Parent { get; set; }
        public int? idKid { get; set; }
        [ForeignKey("idKid")]
        public virtual Kid Kid { get; set; }
    }
}
