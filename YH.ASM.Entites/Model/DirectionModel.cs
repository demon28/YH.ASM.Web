using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
   public class DirectionModel
    {

        public long USER_ID { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USER_NAME { get; set; }

        public string WORK_ID { get; set; }
        public string DEPARTMENT { get; set; }
        public string DTNAME { get; set; }
        public string MOBILE { get; set; }

        /// <summary>
        /// 当月出差天数
        /// </summary>
        public int MOUNTHCOUNT { get; set; }



    }
}
