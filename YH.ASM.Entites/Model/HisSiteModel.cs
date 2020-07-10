using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
   public class HisSiteModel
    {


        public int SID { get; set; }
        public string PRE_USER { get; set; }
        public string NEXT_USER { get; set; }
        public string CREATETIME { get; set; }
        public string HISREAMRKS { get; set; }
        public string NEXT_STATUS { get; set; }
        public int TID { get; set; }


        /// <summary>
        /// 预计完成时间
        /// </summary>
        public string ENDDATE { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string DESCRIPTION { get; set; }
        public string REAMRKS { get; set; }

    }
}
