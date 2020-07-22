using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.ASM.PushService.Model
{
   public class PushSupportUserModel
    {

        /// <summary>
        /// 接收人id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 接收人工号
        /// </summary>
        public string WorkId { get; set; }


        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string Name { get; set; }

    
        /// <summary>
        /// 推送内容
        /// </summary>
        public string Content { get; set; }
    
    }
}
