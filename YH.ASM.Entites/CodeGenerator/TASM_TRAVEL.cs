using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TASM_TRAVEL")]
    public partial class TASM_TRAVEL
    {
           public TASM_TRAVEL(){


           }
        /// <summary>
        /// Desc:主键id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(OracleSequenceName = "SEQ_TASM_TRAVEL", IsPrimaryKey = true)]
        public int TRAID {get;set;}

           /// <summary>
           /// Desc:用户id，外键用户表tasm_user
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int USERID {get;set;}

           /// <summary>
           /// Desc:项目id，外键项目表，前期可为空
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PROJECTID {get;set;}

           /// <summary>
           /// Desc:客户id，外键客户表，前期可为空
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CUSTOMERID {get;set;}

           /// <summary>
           /// Desc:省id，外键tnet_area  出差的目的省
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PROVINCEID {get;set;}

           /// <summary>
           /// Desc:市id，外键tnet_area  出差的目的省
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CITYID {get;set;}

           /// <summary>
           /// Desc:区id，外键tnet_area  出差的目的省
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? AREAID {get;set;}

           /// <summary>
           /// Desc:数据类型（暂定上午打卡，下午打卡两种类型）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? TYPE {get;set;}

           /// <summary>
           /// Desc:售后工单表id， 前期可为空
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SUPPORTID {get;set;}

           /// <summary>
           /// Desc:经度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double? LONGITUDE {get;set;}

           /// <summary>
           /// Desc:纬度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double? LATITUDE {get;set;}

           /// <summary>
           /// Desc:简报
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CONTENT {get;set;}

           /// <summary>
           /// Desc:状态 （暂定 已批准，未批准 冗余）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? STATUS {get;set;}

           /// <summary>
           /// Desc:逻辑删除
           /// Default:
           /// Nullable:False
           /// </summary>           
           public short ISDEL {get;set;}

           /// <summary>
           /// Desc:数据创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CREATETIME {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REMARKS {get;set;}

    }
}
