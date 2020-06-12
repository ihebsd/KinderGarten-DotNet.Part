using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PagedList;

namespace Solution.Web.Models
{
    public class GeoLocationModel
    {
        [Key]
        [ForeignKey("User")]
        public int? idGeo { get; set; }
        public string Address { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public virtual User User { get; set; }
    }
}