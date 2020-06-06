using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.Model;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    //动向记录
    public class DirectionController : ControllerBase.ControllerBase
    {

        [AuthRight]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult List(string keyword,int pageIndex, int pageSize) {

            TASM_TRAVELManager manager = new TASM_TRAVELManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;
           
           
            List<DirectionModel> list = new List<DirectionModel>();
            manager.ListBaseUser(DateTime.Now, keyword, ref p, ref list);
            return SuccessResultList(list, p);

        }

        public IActionResult ListCander(int userid,string date)
        {
            DateTime month = DateTime.Parse(date);

            TASM_TRAVELManager manager = new TASM_TRAVELManager();
            List<DirectionCanderModel> list = new List<DirectionCanderModel>();
            manager.ListByMonth(userid, month, ref list);
            
            return SuccessResultList(list);

        }


        public IActionResult MonthView() {

            return View();
        }

        public IActionResult Canlder()
        {

            return View();
        }
        

    }
}