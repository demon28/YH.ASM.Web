using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_ROLE_RIGHT")]
    public partial class TPMS_ROLE_RIGHT
    {
           public TPMS_ROLE_RIGHT(){


           }
        /// <summary>
        /// Desc:权限ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_ROLE_RIGHTE", IsPrimaryKey = true)]
        public long RIGHT_ID {get;set;}

           /// <summary>
           /// Desc:ROLE_ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? ROLE_ID {get;set;}

           /// <summary>
           /// Desc:关联类型[1：TPMS_PAGE，2：TPMS_ROLE，3：TPMS_FUNCTION]
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long MEMBER_TYPE {get;set;}

           /// <summary>
           /// Desc:关联ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long MEMBER_ID {get;set;}

           /// <summary>
           /// Desc:是否有权限[0：无，1：有]
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long HAVE_RIGHT {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CRETAE_TIME {get;set;}

    }
}
