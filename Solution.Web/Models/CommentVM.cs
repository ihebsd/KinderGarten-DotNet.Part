using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public string post { get; set; }
        public string imageCom { get; set; }
        [DataType(DataType.Date)]

        public DateTime dateCom { get; set; }
        public int PublicationFK { get; set; }

        public string nomUser { get; set; }

    }
}