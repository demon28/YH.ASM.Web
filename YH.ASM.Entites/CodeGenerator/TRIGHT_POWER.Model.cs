using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  权限表
    ///</summary>
    public class   TRIGHT_POWER
    {

       public TRIGHT_POWER()
       {
      
       }

        ///<summary>
        ///描述：ID
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_POWER")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：权限类型，1页面，2功能
        ///</summary>
        public System.Int32 POWERTYPE { get; set; }
        
        ///<summary>
        ///描述：权限名称
        ///</summary>
        public System.String POWERNAME { get; set; }
        
        ///<summary>
        ///描述：
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








