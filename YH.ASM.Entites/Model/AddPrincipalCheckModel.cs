using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Entites.Model
{
    public class AddPrincipalCheckModel:TASM_SUPPORT_PRINCIPAL
    {

        /// <summary>
        /// 工单流程节点
        /// </summary>
        public int SUPPORTSTATUS { get; set; }


        
        /// <summary>
        /// 下一处理人
        /// </summary>
        public int NEXTUSER { get; set; }

        /// <summary>
        /// 当前处理人，对应的处理表id
        /// </summary>

        public int PERSONALID { get; set; }

        /// <summary>
        /// 推送人员
        /// </summary>

        public TASM_SUPPORT_PUSH Push { get; set; }


    }
}
