using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  pmc处理问题表
    ///</summary>
    public class   TASM_SUPPORT_PMC
    {

       public TASM_SUPPORT_PMC()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [SugarColumn(IsPrimaryKey = true,OracleSequenceName = "SEQ_TASM_SUPPORT_PMC")]
        public System.Int32 ID { get; set; }
        
        ///<summary>
        ///描述：bom图纸
        ///</summary>
        public System.String BOM { get; set; }
        
        ///<summary>
        ///描述：是否需要下单
        ///</summary>
        public System.Int32 ISBOOK { get; set; }
        
        ///<summary>
        ///描述：下单日期
        ///</summary>
        public System.DateTime BOOKDATE { get; set; }
        
        ///<summary>
        ///描述：下单人
        ///</summary>
        public System.String BOOKUSER { get; set; }
        
        ///<summary>
        ///描述：物料订单号
        ///</summary>
        public System.String BOOKNO { get; set; }
        
        ///<summary>
        ///描述：交付时间
        ///</summary>
        public System.DateTime DELIVERY { get; set; }
        
        ///<summary>
        ///描述：发货时间
        ///</summary>
        public System.DateTime SENDDATE { get; set; }
        
        ///<summary>
        ///描述：发货单号
        ///</summary>
        public System.String SENDNO { get; set; }
        
        ///<summary>
        ///描述：收货人
        ///</summary>
        public System.String CONSIGNEE { get; set; }
        
        ///<summary>
        ///描述：状态
        ///</summary>
        public System.Int32 STATUS { get; set; }
        
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public System.DateTime CREATETIME { get; set; }
        
        ///<summary>
        ///描述：归属于哪个处理 数据外键 tasm_support
        ///</summary>
        public System.Int32 SID { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public System.String REMARKS { get; set; }
        

    }
 }








