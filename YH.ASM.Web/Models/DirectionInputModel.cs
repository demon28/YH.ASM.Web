using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.Models
{
    public class DirectionInputModel:ApiModelBase
    {

        public int userId { get; set; }

        public int projecId { get; set; }

        public int customerId { get; set; }


        public int type { get; set; }


        public int supportId { get; set; }

        public decimal longitude { get; set; }
        public decimal latitude { get; set; }

        public string content { get; set; }

        public string address { get; set; }

        public DateTime date { get; set; }


        public string projectName { get; set; }
        public string customerName { get; set; }
        public string supportName { get; set; }
    }
}
