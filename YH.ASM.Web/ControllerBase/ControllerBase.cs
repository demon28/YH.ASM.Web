using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NPOI.OpenXmlFormats.Dml;

namespace YH.ASM.Web.ControllerBase
{
    public class ControllerBase : Controller
    {



        public string Work_Id {
            get
            {
              return  GetUser().WORK_ID;
            }
        }

        public string User_Name
        {
            get
            {
                return GetUser().USER_NAME;
            }
        }
        public string User_Phone
        {
            get
            {
                return GetUser().MOBILE;
            }
        }
        public string User_Sex
        {
            get
            {
                return GetUser().USER_SEX;
            }
        }

        public int User_Id
        {
            get
            {
                return GetUser().USER_ID;
            }
        }

        public SysUserInfo GetUser() {
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            var json = userinfo.FindFirst("name");

            SysUserInfo usermodel = JsonConvert.DeserializeObject<SysUserInfo>(json.Value);
            return usermodel;
        }


        public JsonResult FailMessage(string msg = "失败") {
            return Json(new { Success = false, Code = 0, Message = msg });
        }

        public JsonResult SuccessMessage(string msg = "成功")
        {
            return Json(new { Success = true, Code = 1, Message = msg });
        }


        public JsonResult SuccessResult<T>(T t, string msg = "成功") {


            return Json(new { Success = true, Code = 1, Message = msg, Content = t });
        
        }


        public JsonResult SuccessResultList<T>(List<T> list, SqlSugar.PageModel page, string msg = "成功")
        {

            var Total = page.PageCount % page.PageSize > 0 ? page.PageCount / page.PageSize + 1 : page.PageSize;

            return Json(new { Success = true, Code = 1, Message = msg, PageIndex = page.PageIndex, PageSize = page.PageSize, PageCount = page.PageCount, PageTotal = Total, Content = list });
        }


        public JsonResult SuccessResultList<T>(List<T> list, string msg = "成功")
        {

            return Json(new { Success = true, Code = 1, Message = msg,  Content = list });
        }

    }


    public class SysUserInfo{


        public int USER_ID { get; set; }
        public string WORK_ID { get; set; }

        public string USER_NAME { get; set; }

        public string MOBILE { get; set; }

        public string USER_SEX { get; set; }




    }
}
