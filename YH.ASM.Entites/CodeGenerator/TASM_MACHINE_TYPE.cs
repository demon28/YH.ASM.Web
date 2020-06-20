using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    /// 设备类型表
    /// </summary>
    public class TASM_MACHINE_TYPE
    {
        /// <summary>
        /// 设备类型表
        /// </summary>
        public TASM_MACHINE_TYPE()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String NAME { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public System.Int16 STATUS { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public System.DateTime? CREATETIME { get; set; }

        /// <summary>
        /// 常规安装天数
        /// </summary>
        public System.Int32? INSTALLDAYS { get; set; }

        /// <summary>
        /// 常规调试天数
        /// </summary>
        public System.Decimal? DEBUGDAYS { get; set; }

        /// <summary>
        /// 常规验收天数
        /// </summary>
        public System.Decimal? CHECKDAYS { get; set; }
    }
}
