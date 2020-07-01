using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  页面元素表
    ///</summary>
    public class   TRIGHT_PAGE
    {

       public TRIGHT_PAGE()
       {
      
       }

        ///<summary>
        ///描述：ID
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_PAGE")]
        public System.Decimal ID { get; set; }
        
        ///<summary>
        ///描述：页面名称
        ///</summary>
        public System.String PAGENAME { get; set; }
        
        ///<summary>
        ///描述：页面地址
        ///</summary>
        public System.String PAGEURL { get; set; }
        

    }
 }








