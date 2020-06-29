using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
   public class HisPmcModel
    {


        public int SID { get; set; }
        public string PRE_USER { get; set; }
        public string NEXT_USER { get; set; }
        public string CREATETIME { get; set; }
        public string REAMRKS { get; set; }
        public string NEXT_STATUS { get; set; }
        public int TID { get; set; }


        public string BOM { get; set; }
        public string ISBOOK { get; set; }
        public string BOOKDATE { get; set; }
        public string BOOKUSER { get; set; }

        public string BOOKNO { get; set; }
        public string DELIVERY { get; set;  }


        public string SENDDATE { get; set; }

        public string SENDNO { get; set; }
        public string CONSIGNEE { get; set; }
        public string REMARKS { get; set; }
        

    }
}
