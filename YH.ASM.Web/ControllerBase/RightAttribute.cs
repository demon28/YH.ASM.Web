using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.ControllerBase
{
    public class RightAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext Context)
        {

            //TODO: 1.所有用户进来，先查有没有普通用户角色？  没有则创建一个普通用户角色
            //      2.判断是不是ajax，提交？  非ajax提交则判断 为访问页面， 没有这个页面权限 则跳转到 界面，是ajax 则返回json ：code 405
            //      3. 有权限则什么都不做

            //TODO: A，数据库表中，先添加 普通用户 和 超级管理员 两个（角色 or 用户组）



        }




    }
}
