using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{
    /// <summary>
    /// 工单
    /// </summary>
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}