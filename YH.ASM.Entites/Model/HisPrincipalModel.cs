using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
   public class HisPrincipalModel
    {

        public int SID { get; set; }
        public string PRE_USER { get; set; }
        public string NEXT_USER { get; set; }
        public string CREATETIME { get; set; }
        public string HISREAMRKS { get; set; }
        public string NEXT_STATUS { get; set; }
        public int TID { get; set; }


        /// <summary>
        /// 实际完成时间
        /// </summary>
        public string ENDDATE { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string CHECKUSER { get; set; }

        //审核描述
        public string RESULT { get; set; }

        
        public string REMARKS { get; set; }

        public int STATUS { get; set; }

    }
}
