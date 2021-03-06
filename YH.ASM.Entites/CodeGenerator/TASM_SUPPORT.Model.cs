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
        ///描述：流程节点 0，已提交，1，处理中(PMC跟进)，2，已处理（无需PMC），3，已处理(PMC完成)，4，待审核，5未完成，6,已完成  7，已拒绝
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
 
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：外键 处理信息 中间表 
        ///</summary>
        public System.Int32 MEMBERID { get; set; }
        
        ///<summary>
        ///描述：工单状态： 0.待办，1处理中，2，已完成
        ///</summary>
        public System.Int32 STATE { get; set; }

        ///<summary>
        ///描述：问题机型 外键指向，设备表tasm_machine
        ///</summary>
        public System.Int32 MID { get; set; }

        ///<summary>
        ///描述：工单编号，项目编号+三位数
        ///</summary>
        public System.String CODE { get; set; }


    }
 }







