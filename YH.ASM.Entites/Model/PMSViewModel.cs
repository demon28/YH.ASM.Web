using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class PMSViewModel
    {

       public  int  USER_ID{get;set;}
        public int ROLE_ID { get; set; }
        public int APP_ID { get; set; }
        public int RIGHT_ID { get; set; }

        public int FUNC_ID { get; set; }
        public string  FUNC_NAME { get; set; }
        public string ROLE_NAME { get; set; }

        public string PAGE_NAME { get; set; }
        public string PAGE_URL { get; set; }

        public string MEMBER_TYPE { get; set; }
    }
}
