using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单处理人变更历史
    ///</summary>
    public class   TASM_SUPPORT_HIS
    {

       public TASM_SUPPORT_HIS()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_HIS")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：工单id
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：变更前的处理人id 外键tasm_user表
        ///</summary>
        public System.Int32 PRE_USER { get; set; }
        
        ///<summary>
        ///描述：变更后的处理人id 外键tasm_user表
        ///</summary>
        public System.Int32 NEXT_USER { get; set; }
        
        ///<summary>
        ///描述：变更时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








