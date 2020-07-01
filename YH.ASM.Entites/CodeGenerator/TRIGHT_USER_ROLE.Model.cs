using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  用户角色关联表
    ///</summary>
    public class   TRIGHT_USER_ROLE
    {

       public TRIGHT_USER_ROLE()
       {
      
       }

        ///<summary>
        ///描述：
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_USER_ROLE")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：用户ID
        ///</summary>
        public System.Int32 USERID { get; set; }
        
        ///<summary>
        ///描述：角色ID
        ///</summary>
        public System.Int32 ROLEID { get; set; }
        

    }
 }








