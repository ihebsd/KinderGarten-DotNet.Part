using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Solution.Web.Models

{
    
    public class UserLogin
    {
        [Key]
        [ForeignKey("GeoLocation")]
        public int id { get; set; }
        [Display(Name="Email")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email is required")]
        public string email { get; set; }

       

        [Required(AllowEmptyStrings =false,ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        public virtual GeoLocation GeoLocation { get; set; }


        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}