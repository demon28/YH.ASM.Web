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
using YH.ASM.Web.ControllerBase;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class PeopleController : ControllerBase.ComControllerBase
    {

        [Right(PowerName ="人员信息")]
        public IActionResult Index()
        {
            return View();
        }

        
         public IActionResult FillUser()
        {
            return View();
        }

        [Right(PowerName = "查询")]
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

        [Right(Ignore =true)]
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

            row1.CreateCell(2).SetCellValue("部门");
            row1.CreateCell(3).SetCellValue("职务");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("性别");
            row1.CreateCell(6).SetCellValue("状态");
            row1.CreateCell(7).SetCellValue("入职时间");

            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].WORK_ID);
                rowTemp.CreateCell(1).SetCellValue(list[i].USER_NAME);

                rowTemp.CreateCell(2).SetCellValue(list[i].DEPARTMENT);
                rowTemp.CreateCell(3).SetCellValue(list[i].DTNAME);
                rowTemp.CreateCell(4).SetCellValue(list[i].MOBILE);
                rowTemp.CreateCell(5).SetCellValue(list[i].USER_SEX==0?"男":"女");
                rowTemp.CreateCell(6).SetCellValue(list[i].USER_STATUS==0?"在职":(list[i].USER_STATUS == 1?"离职":"锁定"));
                rowTemp.CreateCell(7).SetCellValue(list[i].COMEDATE.ToString("yyyy-MM-dd"));
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
