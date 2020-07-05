using System;
using System.Collections.Generic;
using System.IO;
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
    public class SupportController : ApiControllerBase
    {
        
        [WebApi]
        [HttpPost]
        public IActionResult Create(SupportCreateModel model)
        {
            Facade.SupportFacade support = new Facade.SupportFacade();
            if (!support.Create(model))
            {
                return FailMessage(support.Msg);
            }
            return SuccessMessage("创建工单成功！");

        }



    }
}
