using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.SSO.Models;

namespace YH.ASM.SSO.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;

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
        public  async Task<IActionResult> Login(LoginInputModel model)
        {
            //当登录提交给后台的model为null，则返回错误信息给前台
            if (model == null)
            {
                //这里我只是简单处理了
                return View();
            }

            //这里同理，当信息不完整的时候，返回错误信息给前台
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                //这里只是简单处理了
                return View();
            }
            //model.Username == "123" && model.Password == "123456"
            //if里面的是验证账号密码，可以用自定义的验证，
            //我这里使用的是TestUserStore的的验证方法，  
            //TODO:改成数据库验证
            if (_users.FindByUsername(model.Username) != null && _users.ValidateCredentials(model.Username, model.Password))
            {
                var user = _users.FindByUsername(model.Username);
                //配置Cookie
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                };

                //使用IdentityServer的SignInAsync来进行注册Cookie
                await HttpContext.SignInAsync(user.SubjectId, model.Username);

                //使用IIdentityServerInteractionService的IsValidReturnUrl来验证ReturnUrl是否有问题
                if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return View();
            }
            return View();


        }

        public IActionResult LoginOut()
        {
            return View();
        }

    }
}
