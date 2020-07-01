using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  用户角儿表
    ///</summary>
    public class   TRIGHT_ROLE
    {

       public TRIGHT_ROLE()
       {
      
       }

        ///<summary>
        ///描述：id
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TRIGHT_ROLE")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：角色名称
        ///</summary>
        public System.String ROLENAME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








