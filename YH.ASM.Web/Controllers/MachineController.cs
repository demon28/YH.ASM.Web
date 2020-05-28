using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{
    /// <summary>
    /// 设备履历
    /// </summary>
    public class MachineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}