using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.ControllerBase
{
    public class RightAttribute : ActionFilterAttribute
    {
        public bool Ignore { get; set; }
        public string PowerName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            base.OnActionExecuting(Context);


            //先取出登录用户id；

            int userid = int.Parse(Context.HttpContext.User.FindFirst("USER_ID").Value);


            //如果是初次登录，再系统中没有任何角色 则给用户 分配 一个默认角色，数据库id为1，1为普通会员
            TRIGHT_USER_ROLE_Da userrole = new TRIGHT_USER_ROLE_Da();

            if (userrole.CurrentDb.AsQueryable().Where(s => s.USERID == userid).Count() <= 0)
            {
                TRIGHT_USER_ROLE userolemodel = new TRIGHT_USER_ROLE()
                {
                    ROLEID = 1,   //默认1为普通会员
                    USERID = userid
                };
                userrole.CurrentDb.Insert(userolemodel);
            }



            //如果Ignore 为true 则表示不检查权限，这里只给他初次登录分配 普通会员角色
            if (Ignore)
            {
                return;
            }




            //获取当前页面 或 功能 的路由地址

            var areaName = string.Empty; 
            var controllerName = string.Empty;
            var actionName = string.Empty;

            if (Context.ActionDescriptor.RouteValues.ContainsKey("area"))
            {
                areaName = Context.ActionDescriptor.RouteValues["area"].ToString();
            }
            if (Context.ActionDescriptor.RouteValues.ContainsKey("controller"))
            {
                controllerName = Context.ActionDescriptor.RouteValues["controller"].ToString();
            }
            if (Context.ActionDescriptor.RouteValues.ContainsKey("action"))
            {
                actionName = Context.ActionDescriptor.RouteValues["action"].ToString();
            }



            var page = "/" + controllerName + "/" + actionName;

            if (!string.IsNullOrEmpty(areaName))
            {
                page = "/" + areaName + page;
            }




            //判断请求的 为访问页面 还是 请求功能操作 Ajax请求为功能， 非ajax请求为访问页面
            var isAjax = Context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";




            //判断该页面或操作，是否有再数据库配置过
            TRIGHT_POWER_Da pwmanager = new TRIGHT_POWER_Da();

            var HasPage= pwmanager.Db.Queryable<TRIGHT_POWER>().Where(s => s.PAGEURL.ToLower()==page.ToLower()).Count() <= 0;

            //该页面再数据库未配置
            if (HasPage)
            {

                TRIGHT_POWER powermodel = new TRIGHT_POWER
                {
                    CONTROLLER = controllerName,
                    ACTION = actionName,
                    AREA = areaName,
                    POWERNAME = PowerName,
                    PAGEURL = page.ToLower()
                };

                if (isAjax)
                {
                    // 添加一个功能功能操作的权限
                    var m = pwmanager.Db.Queryable<TRIGHT_POWER>().Where(s => s.CONTROLLER == controllerName && s.POWERTYPE == (int)PowerType.页面访问).First();

                    powermodel.PARENTID = m.ID;
                    powermodel.POWERTYPE = (int)PowerType.功能操作;
                  
                }
                else
                {
                    //添加一个 页面访问 权限
                    powermodel.PARENTID = 0;
                    powermodel.POWERTYPE = (int)PowerType.页面访问;
                 
                }

                pwmanager.CurrentDb.Insert(powermodel);

            }



            //如果全局配置忽略权限，则忽略检测
            if (Entites.AppConfig.IgnoreAuthRight)
            {
                return;
            }


            //该用户存在该页面权限
            if (userrole.ListByVm(userid, page).Count() > 0)
            {
                return;
            }


            //是否ajax请求，是ajax 则判定为 请求操作， 非ajax则判定为 访问页面
            if (isAjax)
            {

                Context.Result = new JsonResult(new { Success = false, Code = 405, Message = "您没有该功能操作权限！" });
                return;

            }

            //跳转配置的页面
            Context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "UserRight",
                action = "NoPermission"
            })); 
            
            return;







        }




     


    }
}
