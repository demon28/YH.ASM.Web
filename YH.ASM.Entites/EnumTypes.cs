using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites
{




    /// <summary>
    /// 工单状态
    /// </summary>
    public enum SupportPersnalStatus
    {
        待办 = 0,
        处理中 = 1,
        已完成 = 2,
    }

    /// <summary>
    /// 工单状态
    /// </summary>
    public enum SupprotWatchState
    {
        待办 = 0,
        处理中 = 1,
        已完成 = 2,
        全部 = 3
    }
    /// <summary>
    /// 工单查看类型
    /// </summary>
    public enum SupprotWatchType
    {
        全部 = 0,
        创建人 = 1,
        处理人 = 2

    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PowerType
    {
        页面访问 = 1,
        功能操作 = 2
    }

    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ProjectStatus
    {
        正常 = 0,
        超前 = 1,
        延迟 = 2
    }

    public enum SupportHisType
    {
        创建工单 = 0,    //
        技术处理 = 1,  //
        PMC跟进 = 2,       //
        现场处理 = 3,   //
        负责人审核 = 4

    }

    public enum SupportendPoint
    {

        创建管理表_责任人处理 = 0,
        分析完成_售后内勤维护 = 1,
        分析完成_现场整改 = 2,
        售后内勤维护完成_现场整改 = 3,
        现场整改_现场负责人审核 = 4,
        现场负责人审核_驳回再整改 = 5,
        现场负责人审核_已完成 = 6,
        已拒绝 = 7


    }



    public enum SupportProblemType
    {
        操作调试 = 0,
        来料质量 = 1,
        商务问题 = 2,
        设计问题 = 3,
        装配误差 = 4
    }

    public enum SupportProblemLevel
    {
        五级 = 0,
        四级 = 1,
        三级 = 2,
        二级 = 3,
        一级 = 4
    }

}
