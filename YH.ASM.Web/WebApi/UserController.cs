using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase.ControllerBase
    {

        [HttpGet("UserList")]
        public JsonResult UserList([FromQuery] ListUserApiModel model)
        {

            DataAccess.TASM_USERManager manager = new DataAccess.TASM_USERManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            List<TASM_USER> list = new List<TASM_USER>();

            manager.ListByDept(model.keywords, ref p, ref list);

            return SuccessResultList(list, p);

        }


    }
}
