using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    /// 客户表
    /// </summary>
    public class TASM_CUSTOMER
    {
        /// <summary>
        /// 客户表
        /// </summary>
        public TASM_CUSTOMER()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_CUSTOMER")]
        public System.Int32 CID { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public System.String NAME { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public System.String PHONE { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public System.String EMIAL { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public System.String ADRESS { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public System.Int16 STATUS { get; set; }

        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public System.Int16? ISDEL { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CREATETIME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String REMARKS { get; set; }
    }
}
