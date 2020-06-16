using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.Controllers
{
    //项目履历
    public class ProjectController : ControllerBase.ControllerBase
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddAndUpdate()
        {
            return View();
        }




        [HttpPost]
        public IActionResult List(string keywords,int pageIndex, int pageSize)
        {
            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            List<TASM_PROJECT> list = new List<TASM_PROJECT>();
            manager.ListByWhere(keywords, ref p, ref list);

            return SuccessResultList(list, p);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }


    }
}