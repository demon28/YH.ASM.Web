using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  角色与权限关联表
    ///</summary>
    public class   TRIGHT_ROLE_POWER
    {

       public TRIGHT_ROLE_POWER()
       {
      
       }

        ///<summary>
        ///描述：ID
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_ROLE_POWER")]
        public System.Decimal ID { get; set; }
        
        ///<summary>
        ///描述：角色ID
        ///</summary>
        public System.Decimal ROLEID { get; set; }
        
        ///<summary>
        ///描述：权限ID
        ///</summary>
        public System.Decimal POWERID { get; set; }
        

    }
 }








