using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_FUNC_MEMBER")]
    public partial class TPMS_FUNC_MEMBER
    {
           public TPMS_FUNC_MEMBER(){


           }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_FUNC_MEMBER", IsPrimaryKey = true)]
        public long ID {get;set;}

           /// <summary>
           /// Desc:功能ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long FUNC_ID {get;set;}

           /// <summary>
           /// Desc:功能关联ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long MEMBER_ID {get;set;}

           /// <summary>
           /// Desc:关联ID类型[1：TPMS_PAGE，2：TPMS_FUNCTION]
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long MEMBER_TYPE {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CREATETIME {get;set;}

    }
}
