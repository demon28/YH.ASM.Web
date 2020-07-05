using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    //客户信息
    public class CustomerController : ControllerBase.ComControllerBase
    {
        [Right(PowerName = "客户信息")]
        public IActionResult Index()
        {
            return View();
        }


        [Right(Ignore =true)]
        public IActionResult AddAndUpdate()
        {
            return View();
        }
        
        [Right(PowerName ="查询")]
        [HttpPost]
        public IActionResult List(string keywords, int pageIndex, int pageSize)
        {

            DataAccess.TASM_CUSTOMERManager manager = new DataAccess.TASM_CUSTOMERManager();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TASM_CUSTOMER> list = new List<TASM_CUSTOMER>();
            manager.ListByWhere(keywords, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [Right(PowerName = "删除")]
        [HttpPost]
        public IActionResult Delete(int cid)
        {

            TASM_CUSTOMERManager manager = new TASM_CUSTOMERManager();
            if (!manager.CurrentDb.DeleteById(cid))
            {
                return FailMessage();
            }
            return SuccessMessage();


        }

        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(Entites.CodeGenerator.TASM_CUSTOMER model)
        {

            DataAccess.TASM_CUSTOMERManager manager = new DataAccess.TASM_CUSTOMERManager();
            if (!manager.CurrentDb.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

        }

        [Right(PowerName = "添加")]
        public IActionResult Add(Entites.CodeGenerator.TASM_CUSTOMER model)
        {

            model.CREATETIME = DateTime.Now;
            model.ISDEL = 0;
            model.STATUS = 0;

            DataAccess.TASM_CUSTOMERManager manager = new DataAccess.TASM_CUSTOMERManager();
            if (!manager.CurrentDb.Insert(model))
            {
                return FailMessage();
            }
            return SuccessMessage("添加成功");

        }

        [Right(Ignore =true)]
        [HttpPost]
        public IActionResult GetUpdateInfo(int id)
        {
            TASM_CUSTOMERManager manager = new TASM_CUSTOMERManager();
            TASM_CUSTOMER model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);

        }


        [Right(Ignore = true)]
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExportExcel(string keyword = "")
        {
            DataAccess.TASM_CUSTOMERManager manager = new DataAccess.TASM_CUSTOMERManager();


            List<TASM_CUSTOMER> list = new List<TASM_CUSTOMER>();
            manager.ListByWhere(keyword, ref list);

            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("项目履历表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("客户名称");
            row1.CreateCell(1).SetCellValue("手机");

            row1.CreateCell(2).SetCellValue("邮箱");
            row1.CreateCell(3).SetCellValue("地址");
            row1.CreateCell(4).SetCellValue("创建时间");
      

            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].NAME);
                rowTemp.CreateCell(1).SetCellValue(list[i].PHONE);

                rowTemp.CreateCell(2).SetCellValue(list[i].EMIAL);
                rowTemp.CreateCell(3).SetCellValue(list[i].ADRESS);
                rowTemp.CreateCell(4).SetCellValue(list[i].CREATETIME.ToString());
      
            }

            var fileName = "客户信息表" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);

        }



    }
}