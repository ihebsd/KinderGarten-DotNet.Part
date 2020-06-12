using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Publication
    {
        public int PublicationId { get; set; }
        public string titlePub { get; set; }
        public string descriptionPub { get; set; }
        //  public int nbofsharing { get; set; }
        public string imagePub { get; set; }
        public DateTime datePub { get; set; }
        public int nbLike { get; set; }

        public int nbVue { get; set; }
        public int nbDislike { get; set; }
        public Boolean Like { get; set; }
        public Boolean Dislike { get; set; }
        public string file { get; set; }

        public string video { get; set; }


        public int ParentFk { get; set; }
        //public int StatFK { get; set; }



        //prop de navig
        [ForeignKey("ParentFk")]
        public virtual Parent Parent { get; set; }

        //[ForeignKey("StatFK")]
        //public virtual StatPublication StatPublication { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }


    }
}
