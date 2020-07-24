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
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.ControllerBase;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase.ComControllerBase
    {
        [Right(PowerName = "工作台")]
        public IActionResult Index()
        {
            return View();
        }

 
        [HttpPost]
        public IActionResult GetLoginUser()
        {

            return Json(new { UserInfo });
        }



        public IActionResult GetReportCount() {


            TASM_PROJECTManager project = new TASM_PROJECTManager();
            int pcount = project.SelectProjectCount();

            TASM_MACHINEManager machine = new TASM_MACHINEManager();
            int mcount = machine.SelectMachineCount();

            TASM_SUPPORT_Da support = new TASM_SUPPORT_Da();
            int scount = support.SelectSupportCount();

            var details = support.SelectCount();

            int cpnumber =(int) (( Convert.ToDecimal( details.Complete) / Convert.ToDecimal(scount)) * 100);
            return Json(new {ProjectCount=pcount,MachineCount=mcount,SupportCount=scount,SupportWaite=details.Wait,SupportBeing=details.Being,SupportComplete=details.Complete, Cpnumber=cpnumber });

        }

    }
}
