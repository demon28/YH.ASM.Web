using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单负责人审核表
    ///</summary>
    public class   TASM_SUPPORT_PRINCIPAL
    {

       public TASM_SUPPORT_PRINCIPAL()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_PRINCIPAL")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：实际完成时间
        ///</summary>
        public System.DateTime ENDDATE { get; set; }
        
        ///<summary>
        ///描述：审核人员（未使用外键 用户表）
        ///</summary>
        public System.String CHECKUSER { get; set; }
        
        ///<summary>
        ///描述：结果描述
        ///</summary>
        public System.String RESULT { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        
        ///<summary>
        ///描述：该条记录的状态，非工单状态
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
        ///<summary>
        ///描述：工单主键id 外键 tasm_support 
        ///</summary>
        public System.Int32 SID { get; set; }
        

    }
 }








