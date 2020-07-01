using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  权限与页面元素关联表
    ///</summary>
    public class   TRIGHT_POWER_PAGE
    {

       public TRIGHT_POWER_PAGE()
       {
      
       }

        ///<summary>
        ///描述：
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_POWER_PAGE")]
        public System.Decimal ID { get; set; }
        
        ///<summary>
        ///描述：权限ID
        ///</summary>
        public System.Decimal POWERID { get; set; }
        
        ///<summary>
        ///描述：页面元素ID
        ///</summary>
        public System.Decimal PAGEID { get; set; }
        

    }
 }








