using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.Entites.Model;
using YH.ASM.Web.Attribute;
using YH.ASM.Web.ControllerBase;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{ 
    public class MachineController : ApiControllerBase
    {



        [WebApi]
        [HttpPost]
        public IActionResult List(ListMachineInputModel model)
        {

            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel()
            {
                PageIndex = model.pageindex,
                PageSize = model.pagesize
            };

            List<MachineModel> list = new List<MachineModel>();
            manager.ListByWhere(string.Empty, ref p, ref list);

            return SuccessResultList(list, p);


        }

    }
}
