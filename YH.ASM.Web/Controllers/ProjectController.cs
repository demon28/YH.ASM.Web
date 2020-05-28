using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{
    //项目履历
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}