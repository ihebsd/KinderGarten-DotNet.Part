using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string post { get; set; }
        public string imageCom { get; set; }
        public DateTime dateCom { get; set; }

        public string nomUser { get; set; }


        public int PublicationFK { get; set; }

        //prop navig
        [ForeignKey("PublicationFK")]
        public virtual Publication Publication { get; set; }
    }
}
