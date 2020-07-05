using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites
{


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

}
