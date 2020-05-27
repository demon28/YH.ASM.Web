using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult LogOut() {


            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         

            return Redirect("/Home/Index");
        }
       


    }
}
