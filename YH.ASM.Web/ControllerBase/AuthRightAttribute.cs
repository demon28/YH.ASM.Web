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
using YH.ASM.Web.Models;

namespace YH.ASM.Web.ControllerBase
{
    public class AuthRightAttribute : ActionFilterAttribute
    {
        public  override void OnActionExecuting(ActionExecutingContext Context)
        {

          

            //获取当前登录的用户信息

            SysUserInfo info = new SysUserInfo();
            info.USER_ID = int.Parse(Context.HttpContext.User.FindFirst("USER_ID").Value);
            info.WORK_ID = Context.HttpContext.User.FindFirst("WORK_ID").Value;
            info.USER_NAME = Context.HttpContext.User.FindFirst("USER_NAME").Value;
            info.MOBILE = Context.HttpContext.User.FindFirst("MOBILE").Value;
            info.USER_SEX = int.Parse(Context.HttpContext.User.FindFirst("USER_SEX").Value);
            

                        //获取当前页面信息

             var controllerName = Context.ActionDescriptor.RouteValues["controller"];
            var actionName = Context.ActionDescriptor.RouteValues["action"];

            var page = "/" + controllerName + "/" + actionName;

            //查出改用户是否有该界面登录权限
            
            //*1 查用户表对应的角色表
            TPMS_USER_RIGHTManager usermanage = new TPMS_USER_RIGHTManager();

            List<PMSViewModel> pmslist = new List<PMSViewModel>();



            TPMS_PAGEManager pageManage = new TPMS_PAGEManager();
            List<TPMS_PAGE> pagelist = new List<TPMS_PAGE>();
            List<PMSViewModel> pmslist2 = new List<PMSViewModel>();

            pageManage.GetPageAll(ref pagelist);


            var isAjax = Context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            //数据库未配置该页面则允许访问
            if (pagelist.Count(s => s.PAGE_URL.ToLower() == page.ToLower())<=0)
            {
               return;
            }


            //如果该用户没有任何角色则添加 普通用户角色
            List<TPMS_USER_RIGHT> urlist = new List<TPMS_USER_RIGHT>();
            if (!usermanage.ListByUserid(info.USER_ID, ref urlist)) {

                TPMS_USER_RIGHT urmodel = new TPMS_USER_RIGHT();
                urmodel.USER_ID = info.USER_ID;
                urmodel.ROLE_ID = 4;
                urmodel.CREATE_TIME = DateTime.Now;

                usermanage.Insert(urmodel);
            }


            //该用户的所有角色没有改页面权限
            if (!usermanage.ListByPmsPageView(info.USER_ID,page,ref pmslist2)) {


                if (!isAjax)
                {
                    Context.Result = new RedirectResult("/Permission/NoPermission");
                    return;
                }

                Context.Result = new JsonResult(new { Success = false, Code = 405, Message = "您没有该项操作权限！" });

            }

            base.OnActionExecuting(Context);

        }
    } 
}

