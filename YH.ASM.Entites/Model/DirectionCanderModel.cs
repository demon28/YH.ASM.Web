﻿using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Entites.Model
{
    public  class DirectionCanderModel: TASM_TRAVEL
    {

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USER_NAME { get; set; }
        public string USER_ID { get; set; }
        public string WORK_ID { get; set; }

        public string DEPARTMENT { get; set; }
    }
}
