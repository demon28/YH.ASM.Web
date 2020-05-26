using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using YH.ASM.Entites.CodeGenerator;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YH.ASM.Web.Controllers
{
    public class PeopleController : ControllerBase.ControllerBase
    {

        [Authorize]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetList(string keyword,int pageIndex,int pageSize)
        {

            DataAccess.TASM_USERManager tASM_USERManager = new DataAccess.TASM_USERManager();
            PageModel p = new PageModel();
            List<TASM_USER> list = new List<TASM_USER>();
            tASM_USERManager.GetListByWhere(keyword, ref p, ref list);

            return  ListJson<TASM_USER>(list,p);
           
        }





    }
}
