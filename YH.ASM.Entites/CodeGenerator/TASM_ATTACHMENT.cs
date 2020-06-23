using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    /// 项目附件表
    /// </summary>
    public class TASM_ATTACHMENT
    {
        /// <summary>
        /// 项目附件表
        /// </summary>
        public TASM_ATTACHMENT()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_ATTACHMENT")]
        public System.Int32 AID { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public System.String FILENAME { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public System.String URL { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String REAMRKS { get; set; }

        /// <summary>
        /// 归属id
        /// </summary>
        public System.Int32   PID { get; set; }

        /// <summary>
        /// 归属表
        /// </summary>
        public System.Int32 TYPE { get; set; }
        
    }
}
