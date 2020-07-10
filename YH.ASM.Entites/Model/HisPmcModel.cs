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


    
        /// <summary>
        /// 订单编号
        /// </summary>
        public string BOOKNO { get; set; }
        /// <summary>
        /// 交付时间
        /// </summary>
        public string DELIVERY { get; set;  }

        /// <summary>
        /// 发货时间
        /// </summary>
        public string SENDDATE { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        public string SENDNO { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string CONSIGNEE { get; set; }
        public string REMARKS { get; set; }
        

    }
}
