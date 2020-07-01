using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  权限与功能操作关联表
    ///</summary>
    public class   TRIGHT_POWER_OPERATION
    {

       public TRIGHT_POWER_OPERATION()
       {
      
       }

        ///<summary>
        ///描述：
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_POWER_OPERATION")]
        public System.Decimal ID { get; set; }
        
        ///<summary>
        ///描述：权限ID
        ///</summary>
        public System.Decimal POWERID { get; set; }
        
        ///<summary>
        ///描述：操作ID
        ///</summary>
        public System.Decimal OPERATIONID { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








