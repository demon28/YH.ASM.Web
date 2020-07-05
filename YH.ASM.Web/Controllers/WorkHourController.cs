using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{
    public class WorkHourController : ControllerBase.ComControllerBase
    {
        //工时配置
        public IActionResult Index()
        {
            return View();
        }
    }
}