using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.DataAccess
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
        ///描述：权限类型，0,菜单，1页面，2功能
        ///</summary>
        public System.Int32 POWERTYPE { get; set; }
        
        ///<summary>
        ///描述：权限名称
        ///</summary>
        public System.String POWERNAME { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        
        ///<summary>
        ///描述：父级ID
        ///</summary>
        public System.Int32 PARENTID { get; set; }
        
        ///<summary>
        ///描述：完整URL
        ///</summary>
        public System.String PAGEURL { get; set; }
        
        ///<summary>
        ///描述：控制器
        ///</summary>
        public System.String CONTROLLER { get; set; }
        
        ///<summary>
        ///描述：行为，
        ///</summary>
        public System.String ACTION { get; set; }
        
        ///<summary>
        ///描述：排序id
        ///</summary>
        public System.Int32 SORTID { get; set; }
        
        ///<summary>
        ///描述：域
        ///</summary>
        public System.String AREA { get; set; }
        

    }
 }








