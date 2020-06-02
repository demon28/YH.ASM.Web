using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.SSO.Filer;
using YH.ASM.SSO.Models;

namespace YH.ASM.SSO.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : ControllerBase.ControllerBase
    {

        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;

        public AccountController(IClientStore clientStore, IIdentityServerInteractionService interaction, TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(Config.GetTestUsers());
            _interaction = interaction;
            _clientStore = clientStore;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            LoginInputModel vm = new LoginInputModel()
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> LoginByWorkid(LoginInputModel model)
        {


            //当登录提交给后台的model为null，则返回错误信息给前台
            if (model == null)
            {
                //这里我只是简单处理了
                return FailMessage("登录失败！");
            }
            //这里同理，当信息不完整的时候，返回错误信息给前台
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                //这里只是简单处理了
                return FailMessage("登录失败,请输入账号密码登录！");
            }

            //model.Username == "123" && model.Password == "123456"
            //if里面的是验证账号密码，可以用自定义的验证，
            //我这里使用的是TestUserStore的的验证方法，


            TASM_USER usermodel = new TASM_USER();
            TASM_USERManager tASM_USERManager = new TASM_USERManager();

            if (tASM_USERManager.LoginByUser(model.Username, Entites.Tool.MD5.Encrypt(model.Password), ref usermodel))
            {
                //配置Cookie
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                };


               // 方案1
               Claim[] claims = new Claim[]{
                   new Claim("WORK_ID", usermodel.WORK_ID),
                   new Claim("USER_NAME", usermodel.USER_NAME),
                   new Claim("MOBILE", usermodel.MOBILE),
                   new Claim("USER_SEX", usermodel.USER_SEX == 0 ? "男" : "女"),
                   new Claim("COMEDATE", usermodel.COMEDATE.ToString("yyyy-MM-dd"))
               };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);


                var jsons_user ="{ \"USER_ID\":'"+ usermodel.USER_ID + "',\"WORK_ID\":"+ usermodel.WORK_ID + ",\"USER_NAME\":'" + usermodel.USER_NAME + "',\"MOBILE\":'"+ usermodel.MOBILE + "',\"USER_SEX\":'"+ usermodel.USER_SEX + "'}";



                //可行原始方案
                 await HttpContext.SignInAsync("Cookies", jsons_user);

                //使用IIdentityServerInteractionService的IsValidReturnUrl来验证ReturnUrl是否有问题
                if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                {
                    return SuccessMessage(model.ReturnUrl);
                }
                return FailMessage("回调地址不正确");
            }
            return FailMessage("账号或密码错误！");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();
            if (logout.PostLogoutRedirectUri != null)
            {
                return Redirect(logout.PostLogoutRedirectUri);
            }
            var refererUrl = Request.Headers["Referer"].ToString();
            return Redirect(refererUrl);
        }


    }
}
