using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.Controllers
{

    public class HomeController : Controller
    {


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

    
    }
}
