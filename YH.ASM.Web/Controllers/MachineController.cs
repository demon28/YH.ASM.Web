using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    /// <summary>
    /// 设备履历
    /// </summary>
    public class MachineController :  ControllerBase.ComControllerBase
    {
        [Right(PowerName = "设备台账")]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult FillMachine() {
            return View();
        }


        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keywords, int pageIndex, int pageSize)
        {
            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            List<MachineModel> list = new List<MachineModel>();
            manager.ListByWhere(keywords, ref p, ref list);

            return SuccessResultList(list, p);
        }


        [Right(PowerName = "添加台账")]
        [HttpPost]
        public IActionResult Add(TASM_MACHINE model)
        {

            model.CREATETIME = DateTime.Now;
            model.ISDEL = 0;
            model.STATUS = 0;
           
            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();


            manager.Db.BeginTran();
            int mid = manager.CurrentDb.InsertReturnIdentity(model);
            manager.Db.CommitTran();


            model.MID = mid;
            model.SERIAL = model.NAME + "-" + model.MID;
       
            manager.CurrentDb.Update(model);

         
            return SuccessMessage("添加成功");

        }

       
        [Right(PowerName = "修改台账")]
        [HttpPost]
        public IActionResult Update(TASM_MACHINE model)
        {

            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();
            if (!manager.CurrentDb.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

        }
        [Right(PowerName = "删除台账")]
        [HttpPost]
        public IActionResult Delete(int id)
        {

            TASM_MACHINEManager dbContext = new TASM_MACHINEManager();
            if (!dbContext.CurrentDb.Delete(S => S.MID == id))
            {
                return FailMessage();
            }
            return SuccessMessage();


        }

      

        [Right(Ignore =true)]
        [HttpGet]
        public IActionResult ExportExcel(string keyword = "")
        {
            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();


            List<MachineModel> list = new List<MachineModel>();
            manager.ListByWhere(keyword, ref list);

            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("项目履历表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("设备名称");
            row1.CreateCell(1).SetCellValue("设备类型");

            row1.CreateCell(2).SetCellValue("设备序号");
            row1.CreateCell(3).SetCellValue("客户名称");
            row1.CreateCell(4).SetCellValue("合同编号");
            row1.CreateCell(5).SetCellValue("订单时间");

            row1.CreateCell(6).SetCellValue("送货时间");
            row1.CreateCell(7).SetCellValue("送货单号");
            row1.CreateCell(8).SetCellValue("验收时间");

            row1.CreateCell(9).SetCellValue("创建时间");
   


            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].NAME);
                rowTemp.CreateCell(1).SetCellValue(list[i].TYPESNAME);

                rowTemp.CreateCell(2).SetCellValue(list[i].SERIAL);
                rowTemp.CreateCell(3).SetCellValue(list[i].CUSTOMER);
                rowTemp.CreateCell(4).SetCellValue(list[i].CONTRACT);
                rowTemp.CreateCell(5).SetCellValue(list[i].ORDERTIME.Value.ToString("yyyy-MM-dd"));
                rowTemp.CreateCell(6).SetCellValue(list[i].DELIVERYTIME.Value);
                rowTemp.CreateCell(7).SetCellValue(list[i].DELIVERYNUMBER);
                rowTemp.CreateCell(8).SetCellValue(list[i].CHECKTIME.Value);
                rowTemp.CreateCell(9).SetCellValue(list[i].CREATETIME.Value);
         

            }

            var fileName = "设备台账" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);



        }


        [Right(Ignore =true)]
        [HttpPost]
        public IActionResult GetUpdateInfo(int id)
        {
            TASM_MACHINEManager manager = new TASM_MACHINEManager();
            var model = manager.SelectById(id);

            return SuccessResult(model);

        }

       

    }
}