using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TASM_USER> list = new List<TASM_USER>();
            tASM_USERManager.GetListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list,p);
           
        }
        [HttpGet]
        public IActionResult ExportExcel(string keyword = "") {

            DataAccess.TASM_USERManager tASM_USERManager = new DataAccess.TASM_USERManager();

            List<TASM_USER> list = new List<TASM_USER>();
            tASM_USERManager.GetAll(keyword,ref list);


            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("人员信息表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("工号");
            row1.CreateCell(1).SetCellValue("姓名");


            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].WORK_ID);
                rowTemp.CreateCell(1).SetCellValue(list[i].USER_NAME);
          
            }

            var fileName = "人员信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);

        }

    }
}
