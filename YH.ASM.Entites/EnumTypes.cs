﻿using System;
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
        待办=0,
        处理中=1,
        已完成=2,
        全部 = 3
    }
    /// <summary>
    /// 工单查看类型
    /// </summary>
    public enum SupprotWatchType
    {
        全部=0,
        创建人=1,
        处理人=2

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
        现场处理 = 3 ,   //
        负责人审核=4

    }

    public enum SupportendPoint
    {

        创建工单_技术分析 = 0,
        分析完成_PMC跟进 = 1,
        分析完成_现场处理 = 2,
        PMC完成_现场处理 = 3,
        现场处理_负责人审核 = 4,
        负责人审核_未完成 = 5,
        负责人审核_已完成 = 6,
        已拒绝 = 7



    }



}
