using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.Web.ControllerBase
{
    public class AuthRightAttribute : ActionFilterAttribute
    {
        public  override void OnActionExecuting(ActionExecutingContext Context)
        {
            //获取当前登录的用户信息


            var json= Context.HttpContext.User.FindFirst("name");

            SysUserInfo info =  JsonConvert.DeserializeObject<SysUserInfo>(json.Value);

            //获取当前页面信息
         
            var controllerName = Context.ActionDescriptor.RouteValues["controller"];
            var actionName = Context.ActionDescriptor.RouteValues["action"];

            var page = "/" + controllerName + "/" + actionName;

            //查出改用户是否有该界面登录权限
            
            //*1 查用户表对应的角色表
            TPMS_USER_RIGHTManager usermanage = new TPMS_USER_RIGHTManager();

            List<PMSViewModel> list = new List<PMSViewModel>();


            //TODO:出现没有配置的界面 或 action 要处理掉

            if (!usermanage.ListByPmsView(info.USER_ID, ref list))
            {

                Context.Result = new RedirectResult("/Permission/NoPermission");

            }

            if (list.Where(s => s.PAGE_URL.ToLower() == page.ToLower()).Count() <= 0) {

                Context.HttpContext.Response.WriteAsync("<script>alert_danger('您没有改功能权限，请找管理员设置')</script>");
            }

        }
    } 
}
