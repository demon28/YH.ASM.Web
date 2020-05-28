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
    
    public class HomeController : ControllerBase.ControllerBase
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

         [HttpGet]
        public async Task<IActionResult> LoginOut() 
        {

           await HttpContext.SignOutAsync("Cookies");
           await HttpContext.SignOutAsync("oidc");

            return View();
        }

        [HttpPost]
        public IActionResult GetLoginUser()
        {

            return Json(new { workid = Work_Id, sex = User_Sex, phone=User_Sex,name=User_Name});
        }

    }
}
