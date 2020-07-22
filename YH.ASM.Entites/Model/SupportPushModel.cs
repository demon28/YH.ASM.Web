using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
   public class SupportPushModel: SupportModel
    {

        /// <summary>
        /// 创建人工号
        /// </summary>
        public string CreatorWorkId { set; get; }



        /// <summary>
        /// 处理人工号Id
        /// </summary>
        public string ConductorWorkId { get; set; }
    }
}
