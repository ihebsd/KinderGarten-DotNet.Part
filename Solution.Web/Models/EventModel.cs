using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int number_P { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEvent { get; set; }
        public DateTime HeureD { get; set; }
        public DateTime HeureF { get; set; }
        public string Description { get; set; }
        public string image { get; set; }
        public bool AdminConfirmtion { get; set; }
        public bool Testpart { get; set; }
        public int DirecteurFK { get; set; }

        //prop de navig
        [ForeignKey("DirecteurFK")]
        public virtual Directeur Directeur { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }

    }
}