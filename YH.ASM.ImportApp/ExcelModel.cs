using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.ASM.ImportApp
{
    class ExcelModel
    {

        [ImporterHeader(Name ="主键id",IsIgnore =true)]
        public int Sid { get; set; }

        [ImporterHeader(Name = "提出日期")]
        public string Createtime { get; set; }


        [ImporterHeader(Name = "工单问题")]
        public string Content { get; set; }

        [ImporterHeader(Name ="创建人工号")]
        public string CreateWorkId { get; set; }



        [ImporterHeader(Name = "创建人姓名")]
        public string CreateName { get; set; }


        [ImporterHeader(Name = "问题类型")]
        public int SupportType { get; set; }

        [ImporterHeader(Name = "严重程度")]
        public int Severity { get; set; }
    
        [ImporterHeader(Name ="处理人工号")]
        public string ConductorWorkId { get; set; }

        [ImporterHeader(Name = "处理人姓名")]
        public string ConductorName { get; set; }


        [ImporterHeader(Name = "项目名称")]
        public string ProjectName { get; set; }

        [ImporterHeader(Name = "流程节点")]
        public int EndPiont { get; set; }


        [ImporterHeader(Name ="设备编号")]
        public string MachineCode { get; set; }

        [ImporterHeader(Name ="工单个人处理状态")]
        public int ConductorStatus { get; set; }


        //--------------------------------------------------------------------------------------------------//
        
        [ImporterHeader(Name = "是否有内勤维护节点")]
        public string IsDisposerPoint { get; set; } 


        [ImporterHeader(Name = "分析人员工号")]
        public string AnalyzeUserWorkId { get; set; }


        [ImporterHeader(Name = "分析人员姓名")]
        public string AnalyzeUserName { get; set; }


        [ImporterHeader(Name ="解决方案")]
        public string Solution { get; set; }


        [ImporterHeader(Name ="责任人")]
        public string Duty { get; set; }


        [ImporterHeader(Name = "责任方")]
        
        public string Responsible { get; set; }

        [ImporterHeader(Name ="是否下单 ")]
        public string IsOrder { get; set; }

        [ImporterHeader(Name ="Bom图纸")]
        public string Bom { get; set; }

        [ImporterHeader(Name ="下单人")]
        public string OrderMan { get; set; }

        [ImporterHeader(Name ="下单时间 ")]
        public string OrderTime { get; set; }
    
    
        //----------------------------------------------------------//

       [ImporterHeader(Name = "是否PMC处理节点")]
        public string IsPmcPoint { get; set; }

        [ImporterHeader(Name = "物料订单号")]
        public string BookNo { get; set; }

        [ImporterHeader(Name = "交付时间")]
        public string Delivery { get; set; }

        [ImporterHeader(Name ="发货时间")]
        public string SendDate { get; set; }

        [ImporterHeader(Name = "发货单号")]
        public string SendNo { get; set; }

        [ImporterHeader(Name = "收货人")]
        public string Consignee { get; set; }

        [ImporterHeader(Name = "备注")]
        public string Remark { get; set; }

        //---------------------------------------------------------//
        [ImporterHeader(Name = "是否有现场处理节点")]
        public string IsSitePoint { get; set; }

        [ImporterHeader(Name = "完成时间")]
        public string EndDate { get; set; }

        [ImporterHeader(Name = "描述")]
        public string Description { get; set; }


        //--------------------------------------------------------//

        [ImporterHeader(Name ="是否有现场审核节点")]
        public string IsPrincipalPoint { get; set; }


        [ImporterHeader(Name = "实际完成时间")]
        public string FinishDate { get; set; }


        [ImporterHeader(Name = "审核姓名")]
        public string CheckName { get; set; }

        [ImporterHeader(Name = "审核结果")]
        public string CheckResult { get; set; }
    }
}
