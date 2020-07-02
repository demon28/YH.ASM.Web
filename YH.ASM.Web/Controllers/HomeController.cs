using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.Web.ControllerBase;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase.ControllerBase
    {
        [Right]
        public IActionResult Index()
        {
            return View();
        }

        [Right(Ignore =true)]
        [HttpGet]
        public async Task<IActionResult> LoginOut() 
        {

           await HttpContext.SignOutAsync("Cookies");
           await HttpContext.SignOutAsync("oidc");

            return View();
        }
       
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetLoginUser()
        {

            return Json(new { UserInfo });
        }

    }
}
