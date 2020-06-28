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
        
        ///<summary>
        ///描述：变更前的 工单状态
        ///</summary>
        public System.Int32 PRE_STATUS { get; set; }
        
        ///<summary>
        ///描述：变更后的 工单状态
        ///</summary>
        public System.Int32 NEXT_STATUS { get; set; }
        
        ///<summary>
        ///描述：记录属于哪个环节的表，0，tasm_support ， 1，tasm_disposer,    2, tasm_support_pmc，3,tasm_support_site, 4,TASM_SUPPORT_PRINCIPAL
        ///</summary>
        public System.Int32 TYPE { get; set; }
        
        ///<summary>
        ///描述：记录属于哪个表的 哪条数据
        ///</summary>
        public System.Int32 TID { get; set; }
        

    }
 }








