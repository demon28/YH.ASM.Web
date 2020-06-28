using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  现场人员处理表
    ///</summary>
    public class   TASM_SUPPORT_SITE
    {

       public TASM_SUPPORT_SITE()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_SITE")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：工单信息表指向tams_support
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：计划完成时间
        ///</summary>
        public System.DateTime ENDDATE { get; set; }
        
        ///<summary>
        ///描述：问题描述
        ///</summary>
        public System.String DESCRIPTION { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REAMRKS { get; set; }
        

    }
 }








