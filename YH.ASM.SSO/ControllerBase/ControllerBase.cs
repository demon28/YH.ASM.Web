using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace YH.ASM.SSO.ControllerBase
{
    public class ControllerBase: Controller
    {



        public JsonResult FailMessage(string msg = "失败")
        {
            return Json(new { Success = false, Code = 0, Message = msg });
        }

        public JsonResult SuccessMessage(string msg = "成功")
        {
            return Json(new { Success = true, Code = 1, Message = msg });
        }


        public JsonResult SuccessResult<T>(T t, string msg = "成功")
        {


            return Json(new { Success = true, Code = 1, Message = msg, Content = t });

        }


        public JsonResult SuccessResultList<T>(List<T> list, SqlSugar.PageModel page, string msg = "成功")
        {

            var Total = page.PageCount % page.PageSize > 0 ? page.PageCount / page.PageSize + 1 : page.PageSize;

            return Json(new { Success = true, Code = 1, Message = msg, PageIndex = page.PageIndex, PageSize = page.PageSize, PageCount = page.PageCount, PageTotal = Total, Content = list });
        }


        public JsonResult SuccessResultList<T>(List<T> list, string msg = "成功")
        {

            return Json(new { Success = true, Code = 1, Message = msg, Content = list });
        }



    }
}
