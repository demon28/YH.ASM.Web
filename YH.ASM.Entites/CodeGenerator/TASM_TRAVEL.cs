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
    public class TASM_TRAVEL
    {
        /// <summary>
        /// 人员动向表(出差)
        /// </summary>
        public TASM_TRAVEL()
        {
        }

        /// <summary>
        /// 主键id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_TRAVEL")]
        public System.Int32 TRAID { get; set; }

        /// <summary>
        /// 用户id，外键用户表tasm_user
        /// </summary>
        public System.Int32 USERID { get; set; }

        /// <summary>
        /// 项目id，外键项目表，前期可为空
        /// </summary>
        public System.Int32? PROJECTID { get; set; }

        /// <summary>
        /// 客户id，外键客户表，前期可为空
        /// </summary>
        public System.Int32? CUSTOMERID { get; set; }

        /// <summary>
        /// 数据类型（暂定上午打卡，下午打卡两种类型）
        /// </summary>
        public System.Int32? TYPE { get; set; }

        /// <summary>
        /// 售后工单表id， 前期可为空
        /// </summary>
        public System.Int32? SUPPORTID { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public System.Double? LONGITUDE { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public System.Double? LATITUDE { get; set; }

        /// <summary>
        /// 简报
        /// </summary>
        public System.String CONTENT { get; set; }

        /// <summary>
        /// 状态 （暂定 已批准，未批准 冗余）
        /// </summary>
        public System.Int16? STATUS { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public System.Int16 ISDEL { get; set; }

        /// <summary>
        /// 数据创建时间
        /// </summary>
        public System.DateTime CREATETIME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String REMARKS { get; set; }

        /// <summary>
        /// 地址来源于定位
        /// </summary>
        public System.String ADDRESS { get; set; }

        /// <summary>
        /// 第一版 没有{项目表} 的情况下 先将项目变成手动输入
        /// </summary>
        public System.String PROJECT_NAME { get; set; }

        /// <summary>
        /// 第一版 没有{客户表} 的情况下 先将客户变成手动输入
        /// </summary>
        public System.String CUSTOMER_NAME { get; set; }

        /// <summary>
        /// 第一版 没有{工单表} 的情况下 先将工单变成手动输入
        /// </summary>
        public System.String SUPPORT_NAME { get; set; }
    }
}
