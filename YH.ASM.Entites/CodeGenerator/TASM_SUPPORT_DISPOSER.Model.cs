using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单处理表
    ///</summary>
    public class   TASM_SUPPORT_DISPOSER
    {

       public TASM_SUPPORT_DISPOSER()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_DISPOSER")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：处理的工单
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：冗余字段，处理人员，外键指向用户表
        ///</summary>
        public System.Int32 ANALYZEUSER { get; set; }
        
        ///<summary>
        ///描述：分析原因
        ///</summary>
        public System.String ANALYZE { get; set; }
        
        ///<summary>
        ///描述：解决方案
        ///</summary>
        public System.String SOLUTION { get; set; }
        
        ///<summary>
        ///描述：当前信息状态，非工单状态
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
        
        ///<summary>
        ///描述：责任方
        ///</summary>
        public System.String RESPONSIBLE { get; set; }
        
        ///<summary>
        ///描述：责任人
        ///</summary>
        public System.String DUTY { get; set; }


        ///<summary>
        ///描述：Bom图纸
        ///</summary>
        public System.String BOM { get; set; }

        ///<summary>
        ///描述：下单人
        ///</summary>
        public System.Int32 ORDERMAN { get; set; }

        ///<summary>
        ///描述：下单时间
        ///</summary>
        public System.DateTime ORDERTIME { get; set; }

        ///<summary>
        ///描述：是否下单
        ///</summary>
        public System.Int32 ISORDER { get; set; }


    }
}








