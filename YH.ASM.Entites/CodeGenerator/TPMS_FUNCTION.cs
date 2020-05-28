using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_FUNCTION")]
    public partial class TPMS_FUNCTION
    {
           public TPMS_FUNCTION(){


           }
        /// <summary>
        /// Desc:功能ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_FUNCTION", IsPrimaryKey = true)]
        public long FUNC_ID {get;set;}

           /// <summary>
           /// Desc:功能名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FUNC_NAME {get;set;}

           /// <summary>
           /// Desc:归属应用ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long APP_ID {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CREATETIME {get;set;}

           /// <summary>
           /// Desc:图标文件地址或者css class
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ICON {get;set;}

           /// <summary>
           /// Desc:排序索引
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long SORT_INDEX {get;set;}

           /// <summary>
           /// Desc:功能模块唯一ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GUID {get;set;}

    }
}
