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
        /// <summary>
        /// 主键id
        /// </summary>
        [ImporterHeader(Name ="主键id",IsIgnore =true)]
        public int Sid { get; set; }
        /// <summary>
        /// 提出日期
        /// </summary>
        [ImporterHeader(Name = "提出日期")]
        public string Createtime { get; set; }

        /// <summary>
        /// 工单问题
        /// </summary>
        [ImporterHeader(Name = "工单问题")]
        public string Content { get; set; }
        /// <summary>
        /// 创建人工号
        /// </summary>
        [ImporterHeader(Name ="创建人工号")]
        public string CreateWorkId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>

        [ImporterHeader(Name = "创建人姓名")]
        public string CreateName { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        [ImporterHeader(Name = "问题类型")]
        public int SupportType { get; set; }
        /// <summary>
        /// 严重程度
        /// </summary>
        [ImporterHeader(Name = "严重程度")]
        public int Severity { get; set; }
        /// <summary>
        /// 处理人工号
        /// </summary>
        [ImporterHeader(Name ="处理人工号")]
        public string ConductorWorkId { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        [ImporterHeader(Name = "处理人姓名")]
        public string ConductorName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [ImporterHeader(Name = "项目名称")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 流程节点
        /// </summary>
        [ImporterHeader(Name = "流程节点")]
        public int EndPiont { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [ImporterHeader(Name ="设备编号")]
        public string MachineCode { get; set; }
        /// <summary>
        ///工单个人处理状态 
        /// </summary>
        [ImporterHeader(Name ="工单个人处理状态")]
        public int ConductorStatus { get; set; }


        //--------------------------------------------------------------------------------------------------//

        /// <summary>
        /// 是否有内勤维护节点
        /// </summary>
        [ImporterHeader(Name = "是否有内勤维护节点")]
        public string IsDisposerPoint { get; set; }

        /// <summary>
        /// 分析人员工号
        /// </summary>
        [ImporterHeader(Name = "分析人员工号")]
        public string AnalyzeUserWorkId { get; set; }

        /// <summary>
        /// 分析人员姓名
        /// </summary>
        [ImporterHeader(Name = "分析人员姓名")]
        public string AnalyzeUserName { get; set; }



        /// <summary>
        /// 分析原因
        /// </summary>
        [ImporterHeader(Name = "分析原因")]
        public string Analyze { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        [ImporterHeader(Name ="解决方案")]
        public string Solution { get; set; }

        /// <summary>
        /// 责任人     
        /// /// </summary>
        [ImporterHeader(Name ="责任人")]
        public string Duty { get; set; }

        /// <summary>
        /// 责任方
        /// </summary>
        [ImporterHeader(Name = "责任方")]
        public string Responsible { get; set; }

        /// <summary>
        /// 是否下单
        /// </summary>
        [ImporterHeader(Name ="是否下单 ")]
        public string IsOrder { get; set; }

        /// <summary>
        /// Bom图纸
        /// </summary>
        [ImporterHeader(Name ="Bom图纸")]
        public string Bom { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        [ImporterHeader(Name ="下单人")]
        public string OrderMan { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        [ImporterHeader(Name ="下单时间 ")]
        public string OrderTime { get; set; }


        //----------------------------------------------------------//

        /// <summary>
        /// 是否PMC处理节点
        /// </summary>
        [ImporterHeader(Name = "是否PMC处理节点")]
        public string IsPmcPoint { get; set; }
        /// <summary>
        /// 物料订单号
        /// </summary>
        [ImporterHeader(Name = "物料订单号")]
        public string BookNo { get; set; }
        /// <summary>
        /// 交付时间
        /// </summary>
        [ImporterHeader(Name = "交付时间")]
        public string Delivery { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        [ImporterHeader(Name ="发货时间")]
        public string SendDate { get; set; }
        /// <summary>
        /// 发货单号
        /// </summary>
        [ImporterHeader(Name = "发货单号")]
        public string SendNo { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        [ImporterHeader(Name = "收货人")]
        public string Consignee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [ImporterHeader(Name = "备注")]
        public string Remark { get; set; }

        //---------------------------------------------------------//
        /// <summary>
        /// 是否有现场处理节点
        /// </summary>
        [ImporterHeader(Name = "是否有现场处理节点")]
        public string IsSitePoint { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        [ImporterHeader(Name = "完成时间")]
        public string EndDate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [ImporterHeader(Name = "描述")]
        public string Description { get; set; }


        //--------------------------------------------------------//
        /// <summary>
        /// 是否有现场审核节点
        /// </summary>
        [ImporterHeader(Name ="是否有现场审核节点")]
        public string IsPrincipalPoint { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        [ImporterHeader(Name = "实际完成时间")]
        public string FinishDate { get; set; }

        /// <summary>
        /// 审核姓名
        /// </summary>
        [ImporterHeader(Name = "审核姓名")]
        public string CheckName { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        [ImporterHeader(Name = "审核结果")]
        public string CheckResult { get; set; }
    }
}
