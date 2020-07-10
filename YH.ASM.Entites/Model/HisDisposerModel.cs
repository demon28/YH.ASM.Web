using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class HisDisposerModel
    {

        public int SID { get; set; }
        public string PRE_USER { get; set; }
        public string NEXT_USER { get; set; }
        public string CREATETIME { get; set; }
        public string REAMRKS { get; set; }
        public string NEXT_STATUS { get; set; }

        public int TID { get; set; }
        /// <summary>
        /// 分析人
        /// </summary>
        public string ANALYZEUSER { get; set; }

        /// <summary>
        /// 分析原因
        /// </summary>
        public string ANALYZE { get; set; }
        /// <summary>
        /// 解决方案
        /// </summary>
        public string SOLUTION { get; set; }
        /// <summary>
        /// 责任方
        /// </summary>
        public string RESPONSIBLE { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string DUTY { get; set; }

        /// <summary>
        /// bom图纸
        /// </summary>
        public string BOM { get; set; }

        /// <summary>
        /// bom图纸
        /// </summary>
        public int ISORDER { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string ORDERTIME { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public string ORDERMAN { get; set; }
    }
}
