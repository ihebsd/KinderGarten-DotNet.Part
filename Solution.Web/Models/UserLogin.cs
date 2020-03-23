using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models

{
    
    public class UserLogin
    {
        [Key]
        public int id { get; set; }
        [Display(Name="Email")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email is required")]
        public string email { get; set; }

       

        [Required(AllowEmptyStrings =false,ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        



        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}