using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.CodeGenerator
{
  public  class TASM_MAINTAINER
    {

            /// <summary>
            /// 项目售后人员表
            /// </summary>
            public TASM_MAINTAINER()
            {
            }

            /// <summary>
            /// 主键id
            /// </summary>
            [SugarColumn(IsPrimaryKey = true, OracleSequenceName = "SEQ_TASM_MAINTAINER")]
            public System.Int32 MID { get; set; }

            /// <summary>
            /// 人员动向表id 
            /// </summary>
            public System.Int32 TID { get; set; }

            /// <summary>
            /// 用户表id
            /// </summary>
            public System.Int32 UUID { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            public System.Int32 STATUS { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public System.String REMARK { get; set; }

            /// <summary>
            /// 时间
            /// </summary>
            public System.DateTime? CREATETIME { get; set; }
        }



}
