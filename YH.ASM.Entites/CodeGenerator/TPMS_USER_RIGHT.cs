using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_USER_RIGHT")]
    public partial class TPMS_USER_RIGHT
    {
           public TPMS_USER_RIGHT(){


           }
        /// <summary>
        /// Desc:权限ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_USER_RIGHT", IsPrimaryKey = true)]
        public long RIGHT_ID {get;set;}

           /// <summary>
           /// Desc:会员ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long USER_ID {get;set;}

           /// <summary>
           /// Desc:角色ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long ROLE_ID {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CREATE_TIME {get;set;}

           /// <summary>
           /// Desc:过期时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? EXPIRE_TIME {get;set;}

    }
}
