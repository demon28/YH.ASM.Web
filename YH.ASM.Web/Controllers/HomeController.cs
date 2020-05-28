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
using YH.ASM.Web.Models;

namespace YH.ASM.Web.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var workid=  HttpContext.User.Claims.First();
            Console.WriteLine("测试打印一个============" + workid);
            return View();
        }

         [HttpGet]
        public async Task<IActionResult> LoginOut() 
        {

           await HttpContext.SignOutAsync("Cookies");
           await HttpContext.SignOutAsync("oidc");

            return View();
        }
       


    }
}
