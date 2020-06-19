using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    /// <summary>
    /// 工单
    /// </summary>
    public class SupportController : ControllerBase.ControllerBase
    {
        [AuthRight]
        public IActionResult Index()
        {
            return View();
        }

        [AuthRight]
        public IActionResult Create()
        {
            return View();
        }


        




    }
}