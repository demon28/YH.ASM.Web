using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_ROLE")]
    public partial class TPMS_ROLE
    {
           public TPMS_ROLE(){


           }
        /// <summary>
        /// Desc:角色ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_ROLE", IsPrimaryKey = true)]
        public long ROLE_ID {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ROLE_NAME {get;set;}

           /// <summary>
           /// Desc:app编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long APP_ID {get;set;}




           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>        
           public DateTime CREATE_TIME {get;set;}

           /// <summary>
           /// Desc:角色唯一ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GUID {get;set;}

    }
}
