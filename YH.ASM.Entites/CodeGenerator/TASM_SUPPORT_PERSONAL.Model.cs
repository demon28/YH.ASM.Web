using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单个人处理表
    ///</summary>
    public class   TASM_SUPPORT_PERSONAL
    {

       public TASM_SUPPORT_PERSONAL()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_PERSONAL")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：工单id
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：流程节点
        ///</summary>
        public System.Int32 TID { get; set; }
        
        ///<summary>
        ///描述：创建人id
        ///</summary>
        public System.Int32 CID { get; set; }
        
        ///<summary>
        ///描述：处理人id
        ///</summary>
        public System.Int32 DID { get; set; }
        
        ///<summary>
        ///描述：处理状态，0，待办，1处理中，2已完成
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }







