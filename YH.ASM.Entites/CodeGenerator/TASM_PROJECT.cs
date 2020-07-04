using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    /// 项目表
    /// </summary>
    public class TASM_PROJECT
    {
        /// <summary>
        /// 项目表
        /// </summary>
        public TASM_PROJECT()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_PROJECT")]
        public System.Int32 PID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public System.String NAME { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>  
        public System.String CUSTOMER_NAME { get; set; }

        /// <summary>
        /// 出货时间
        /// </summary>
        public System.DateTime? PRODUCTDATE { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public System.String MACHINE { get; set; }

        /// <summary>
        /// 出货数量
        /// </summary>
        public System.Int32? COUNT { get; set; }

        /// <summary>
        /// 安装负责人
        /// </summary>
        public System.String INSTALL_NAME { get; set; }

        /// <summary>
        /// 一级负责人
        /// </summary>
        public System.String INSTALL_LV1 { get; set; }

        /// <summary>
        /// 二级负责人
        /// </summary>
        public System.String INSTALL_LV2 { get; set; }

        /// <summary>
        /// 三级负责人
        /// </summary>
        public System.String INSTALL_LV3 { get; set; }

        /// <summary>
        /// 安装开始时间
        /// </summary>
        public System.DateTime? INSTALL_STARDATE { get; set; }

        /// <summary>
        /// 安装结束时间
        /// </summary>
        public System.DateTime? INSTALL_ENDDATE { get; set; }

        /// <summary>
        /// 安装天数
        /// </summary>
        public System.Int32? INSTALL_DAYS { get; set; }

        /// <summary>
        /// 安装状态，0正常，1超前，2延后
        /// </summary>
        public System.Int16? INSTALL_STATUS { get; set; }

        /// <summary>
        /// 调试开始时间
        /// </summary>
        public System.DateTime? DEBUG_STARDATE { get; set; }

        /// <summary>
        /// 调试结束时间
        /// </summary>
        public System.DateTime? DEBUG_ENDDATE { get; set; }

        /// <summary>
        /// 调试天数
        /// </summary>
        public System.Int32? DEBUG_DAYS { get; set; }

        /// <summary>
        /// 调试状态 0 正常，1超前，2延后
        /// </summary>
        public System.Int16? DEBUG_STATUS { get; set; }

        /// <summary>
        /// 验收开始时间
        /// </summary>
        public System.DateTime? CHECK_STARDATE { get; set; }

        /// <summary>
        /// 验收结束时间
        /// </summary>
        public System.DateTime? CHECK_ENDDATE { get; set; }

        /// <summary>
        /// 倒计时
        /// </summary>
        public System.Int32? COUNT_DOWN { get; set; }

        /// <summary>
        /// 数据状态
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
        /// 逻辑删除
        /// </summary>
        public System.Int16? ISDEL { get; set; }

        /// <summary>
        /// 设备类型 外键设备类型表
        /// </summary>
        public System.Int32? MACHINETYPE { get; set; }

        /// <summary>
        /// 设备 外键设备表
        /// </summary>
        public System.Int32? MACHINET { get; set; }
    }
}
