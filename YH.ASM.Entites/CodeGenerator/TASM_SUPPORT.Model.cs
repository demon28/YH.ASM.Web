using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单信息表
    ///</summary>
    public class   TASM_SUPPORT
    {

       public TASM_SUPPORT()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT")]
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：标题
        ///</summary>
        public System.String TITLE { get; set; }
        
        ///<summary>
        ///描述：描述
        ///</summary>
        public System.String CONTENT { get; set; }
        
        ///<summary>
        ///描述：创建者 外键userid
        ///</summary>
        public System.Int32 CREATOR { get; set; }
        
        ///<summary>
        ///描述：0：操作调试，1来料问题，2商务问题，3设计问题，4装配误差
        ///</summary>
        public System.Int32 TYPE { get; set; }
        
        ///<summary>
        ///描述：严重程度
        ///</summary>
        public System.Int32 SEVERITY { get; set; }
        
        ///<summary>
        ///描述：优先级
        ///</summary>
        public System.Int32 PRIORITY { get; set; }
        
        ///<summary>
        ///描述：发现时间
        ///</summary>
        public System.DateTime FINDATE { get; set; }
        
        ///<summary>
        ///描述：处理人，外键userid
        ///</summary>
        public System.Int32 CONDUCTOR { get; set; }
        
        ///<summary>
        ///描述：外键项目表
        ///</summary>
        public System.Int32 PROJECT { get; set; }
        
        ///<summary>
        ///描述：工单状态 0，已提交，1，处理中，2，已解决，3，已完成，4，已拒绝
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
        ///<summary>
        ///描述：逻辑删除
        ///</summary>
        public System.Int32 ISDEL { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：外键 处理信息 中间表 
        ///</summary>
        public System.Int32 MEMBERID { get; set; }
        

    }
 }








