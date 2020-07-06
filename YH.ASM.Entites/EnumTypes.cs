using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites
{


    /// <summary>
    /// 权限类型
    /// </summary>


    public enum SupprotWatchState
    {
        待办=0,
        处理中=1,
        已完成=2,
        全部 = 3
    }

    public enum SupprotWatchType
    {
        全部=0,
        创建人=1,
        处理人=2

    }
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

}
