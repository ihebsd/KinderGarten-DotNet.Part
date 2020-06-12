using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Parent : User
    {
        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }

        public virtual ICollection<CarPool> CarPools { get; set; }
        public virtual ICollection<Kid> Kids { get; set; }
        public int idGeo { get; set; }
        [ForeignKey("idGeo")]
        public virtual GeoLocation GeoLocation { get; set; }


    }
}
