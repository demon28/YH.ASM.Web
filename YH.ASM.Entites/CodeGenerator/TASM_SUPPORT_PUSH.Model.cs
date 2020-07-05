using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  工单信息推送表
    ///</summary>
    public class   TASM_SUPPORT_PUSH
    {

       public TASM_SUPPORT_PUSH()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_PUSH")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：工单id
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：工单处理人 外键用户表
        ///</summary>
        public System.Int32 CONDUCTOR { get; set; }
        
        ///<summary>
        ///描述：抄送人员，外键用户表id，以逗号分隔
        ///</summary>
        public System.String CC { get; set; }
        
        ///<summary>
        ///描述：流程节点 记录属于哪个环节的表，0，tasm_support ， 1，tasm_disposer,    2, tasm_support_pmc，3,tasm_support_site, 4,TASM_SUPPORT_PRINCIPAL
        ///</summary>
        public System.Int32 POINT { get; set; }
        
        ///<summary>
        ///描述：记录属于哪个表的 哪条数据
        ///</summary>
        public System.Int32 TID { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：状态
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
        ///<summary>
        ///描述：推送消息内容
        ///</summary>
        public System.String CONTENT { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








