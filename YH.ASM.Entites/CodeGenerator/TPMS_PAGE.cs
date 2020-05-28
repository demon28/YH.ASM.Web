using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TPMS_PAGE")]
    public partial class TPMS_PAGE
    {
           public TPMS_PAGE(){


           }
        /// <summary>
        /// Desc:页面ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TPMS_PAGE", IsPrimaryKey = true)]
        public long PAGE_ID {get;set;}

           /// <summary>
           /// Desc:页面名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PAGE_NAME {get;set;}

           /// <summary>
           /// Desc:相对URL
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PAGE_URL {get;set;}

           /// <summary>
           /// Desc:应用ID
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
           /// Desc:备注信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REMARKS {get;set;}

           /// <summary>
           /// Desc:MVC分区名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AREA_NAME {get;set;}

           /// <summary>
           /// Desc:控制器名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CONTROLLER_NAME {get;set;}

           /// <summary>
           /// Desc:视图名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACTION_NAME {get;set;}

           /// <summary>
           /// Desc:路由可选参数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OPTION_VALUE {get;set;}

           /// <summary>
           /// Desc:资源类型[0 - View，1 - API]
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long RESOUCE_TYPE {get;set;}

           /// <summary>
           /// Desc:资源唯一ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GUID {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long SORT_INDEX {get;set;}

    }
}
