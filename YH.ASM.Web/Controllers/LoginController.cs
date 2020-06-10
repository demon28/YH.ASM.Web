using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.Controllers
{
    public class LoginController : ControllerBase.ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// post 登录请求
        /// </summary>
        /// <returns></returns>

        [HttpPost]

        public async Task<IActionResult> LoginByWorkid(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                //这里只是简单处理了
                return FailMessage("登录失败,请输入账号密码登录！");
            }

            //model.Username == "123" && model.Password == "123456"
            //if里面的是验证账号密码，可以用自定义的验证，
            //我这里使用的是TestUserStore的的验证方法，


            TASM_USER usermodel = new TASM_USER();
            TASM_USERManager tASM_USERManager = new TASM_USERManager();

            if (!tASM_USERManager.LoginByUser(userName, Entites.Tool.MD5.Encrypt(password), ref usermodel))
            {
                return FailMessage("账号或密码错误！");
            }


            var claims = new List<Claim>(){
                 new Claim("USER_ID", usermodel.USER_ID.ToString()),
                   new Claim("WORK_ID", usermodel.WORK_ID),
                   new Claim("USER_NAME", usermodel.USER_NAME),
                   new Claim("MOBILE", usermodel.MOBILE),
                   new Claim("USER_SEX", usermodel.USER_SEX.ToString()),
                   new Claim("COMEDATE", usermodel.COMEDATE.ToString("yyyy-MM-dd") )
               };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false

            });

            return SuccessMessage("登录成功！");

        }



        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoginOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login/Index");

        }



    }
}