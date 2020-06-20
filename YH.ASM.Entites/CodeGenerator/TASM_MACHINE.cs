using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    /// 设备信息表
    /// </summary>
    public class TASM_MACHINE
    {
        /// <summary>
        /// 设备信息表
        /// </summary>
        public TASM_MACHINE()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_MACHINE")]
        public System.Int32 MID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String NAME { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public System.String SERIAL { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public System.String CUSTOMER { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        public System.String CONTRACT { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        public System.DateTime? ORDERTIME { get; set; }

        /// <summary>
        /// 送货时间
        /// </summary>
        public System.DateTime? DELIVERYTIME { get; set; }

        /// <summary>
        /// 送货单号
        /// </summary>
        public System.String DELIVERYNUMBER { get; set; }

        /// <summary>
        /// 验收时间
        /// </summary>
        public System.DateTime? CHECKTIME { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public System.Int16? ISDEL { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public System.Int16? STATUS { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CREATETIME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String REMARKS { get; set; }

        /// <summary>
        /// 预计安装天数
        /// </summary>
        public System.Int32? DAYS { get; set; }


        public System.Int32? TYPES { get; set; }

    }
}
