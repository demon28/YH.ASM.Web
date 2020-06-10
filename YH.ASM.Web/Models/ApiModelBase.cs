using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.Models
{
    public class ApiModelBase
    {
        [Required]
        public string SigningKey { get; set; }

    }
}
