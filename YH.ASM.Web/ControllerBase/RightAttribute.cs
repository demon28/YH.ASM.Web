using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.ControllerBase
{
    public class RightAttribute : ActionFilterAttribute
    {
        public bool Ignore { get; set; }

        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            base.OnActionExecuting(Context);

            //TODO: 1.所有用户进来，先查有没有普通用户角色？  没有则创建一个普通用户角色
            //      2.判断是不是ajax，提交？  非ajax提交则判断 为访问页面， 没有这个页面权限 则跳转到 界面，是ajax 则返回json ：code 405
            //      3. 有权限则什么都不做

            //TODO: A，数据库表中，先添加 普通用户 和 超级管理员 两个（角色 or 用户组）

            //https://www.cnblogs.com/myindex/p/9116177.html

            //先取出登录用户id；

            int userid = int.Parse(Context.HttpContext.User.FindFirst("USER_ID").Value);



            TPMS_USER_RIGHTManager tpms_user_rightmanager = new TPMS_USER_RIGHTManager();
            List<TPMS_USER_RIGHT> tpms_user_rightlist = new List<TPMS_USER_RIGHT>();

            //查询该用户是否存在 基本角色，如果一个角色没有就是初次登录，初次登录给他一个普通用户 的角色
            if (!tpms_user_rightmanager.ListByUserid(userid,ref tpms_user_rightlist))
            {
                TPMS_USER_RIGHT tpms_user_rightmodel = new TPMS_USER_RIGHT();
                tpms_user_rightmodel.CREATE_TIME = DateTime.Now;
                tpms_user_rightmodel.ROLE_ID = 1;
                tpms_user_rightmodel.USER_ID = userid;
                tpms_user_rightmanager.Insert(tpms_user_rightmodel);
            }


            //如果Ignore 为true 则表示不检查权限，这里只给他初次登录分配 普通会员角色
            if (Ignore)
            {
                return;
            }

            

            //获取当前页面 或 功能 的路由地址
            var controllerName = Context.ActionDescriptor.RouteValues["controller"];
            var actionName = Context.ActionDescriptor.RouteValues["action"];

            var page = "/" + controllerName + "/" + actionName;


            //判断请求的 为访问页面 还是 请求功能操作 Ajax请求为功能， 非ajax请求为访问页面
            var isAjax = Context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            
            
            if (isAjax)
            {
                if (!HasFucRight(page,userid, tpms_user_rightlist))
                {
                    Context.Result = new JsonResult(new { Success = false, Code = 405, Message = "您没有该项操作权限！" });
                    return;
                }
            }

            if (!HasPageRight(page, userid, tpms_user_rightlist))
            {
                Context.Result = new JsonResult(new { Success = false, Code = 302, Message = "您没有该项操作权限！" });
                return;
            }


          

        }

        //是否拥有页面访问权限
        private bool HasPageRight(string page,int userid, List<TPMS_USER_RIGHT> rolelist)
        {
            TPMS_USER_RIGHTManager tpms_user_rightmanager = new TPMS_USER_RIGHTManager();

            return tpms_user_rightmanager.ListRoleRightPageView(userid, page).Count>0;

        }

        //是否拥有操作功能权限
        private bool HasFucRight(string page, int userid, List<TPMS_USER_RIGHT> rolelist)
        {
            throw new NotImplementedException();
        }
    }
}
