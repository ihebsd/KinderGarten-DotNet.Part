using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class GeoLocation
    {
        [Key]
        public int? idGeo { get; set; }
        public string Address { get; set; }
        public string latlng { get; set; }
        public int? IdParent { get; set; }
        [ForeignKey("idParent")]
        public virtual Parent Parent { get; set; }

    }
}
