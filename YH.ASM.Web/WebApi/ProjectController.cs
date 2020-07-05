using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase.ComControllerBase
    {
        [HttpGet("ProjectList")]
        public JsonResult ProjectList([FromQuery] ApiListModelBase model)
        {

            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            List<ProjectModel> list = new List<ProjectModel>();

            manager.ListByWhere(model.keywords, ref p, ref list);

            return SuccessResultList(list, p);

        }



    }
}
