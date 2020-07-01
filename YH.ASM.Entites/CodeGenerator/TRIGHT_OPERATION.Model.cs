using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  功能操作表
    ///</summary>
    public class   TRIGHT_OPERATION
    {

       public TRIGHT_OPERATION()
       {
      
       }

        ///<summary>
        ///描述：ID
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_OPERATION")]
        public System.Decimal ID { get; set; }
        
        ///<summary>
        ///描述：操作名称
        ///</summary>
        public System.String OPERATIONNAME { get; set; }
        
        ///<summary>
        ///描述：操作编码
        ///</summary>
        public System.String OPERATIONCODE { get; set; }
        
        ///<summary>
        ///描述：拦截URL
        ///</summary>
        public System.String PAGEURL { get; set; }
        
        ///<summary>
        ///描述：操作父ID
        ///</summary>
        public System.Decimal PARENTID { get; set; }
        

    }
 }








