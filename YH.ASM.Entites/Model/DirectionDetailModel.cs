using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class DirectionDetailModel
    {

        public int traid { get; set; }

        public int userid { get; set; }
        public int type { get; set; }
        public int supportid { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string content { get; set; }
        public int status { get; set; }
        public DateTime createtime { get; set; }
        public string user_name { get; set; }
        public string project_name { get; set; }
        public string customer_name { get; set; }

        public string provincen_name { get; set; }
        public string city_name { get; set; }

        public string support_title { get; set; }


        public string department { get; set; }
        public string workid { get; set; }

        public string address { get; set; }
    }                          
}
