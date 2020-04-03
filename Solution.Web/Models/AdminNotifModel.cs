using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public class AdminNotifModel
    {
        [Key]
        public int Id { get; set; }
        public string msg { get; set; }
        public DateTime Datenotif { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId ")]
        public virtual User User { get; set; }
    }
}