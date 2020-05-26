using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YH.ASM.Web.ControllerBase
{
    public class ControllerBase: Controller
    {

        public JsonResult FailMessage(string msg="失败") {
            return Json(new { Success = false,Code=0  ,Message=msg});
        }

        public JsonResult SuccessMessage(string msg="成功")
        {
            return Json(new { Success = true, Code = 1 , Message = msg });
        }

        public JsonResult  SuccessResultList<T>(List<T> list,SqlSugar.PageModel page, string msg="成功")
        {

            return Json(new { Success = true, Code = 1, Message = msg ,PageIndex=page.PageIndex, PageSize=page.PageSize,PageCount= page.PageCount, Content = list } );
        }

    }
}
