using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.Web.WebApi;

namespace YH.ASM.Web.Models
{
    public class LoginInputModel: ApiModelBase
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    
    
    }
}
