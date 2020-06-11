﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
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


        public IActionResult List(string date,string keyword,int pageIndex, int pageSize) {

            TASM_TRAVELManager manager = new TASM_TRAVELManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            DateTime time = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                time = DateTime.Parse(date);
            }
    
           
            List<DirectionModel> list = new List<DirectionModel>();
            manager.ListBaseUser(time, keyword, ref p, ref list);
            return SuccessResultList(list, p);

        }

        public IActionResult ListCander(int userid,string date)
        {
             DateTime time = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                time = DateTime.Parse(date);
            }


            TASM_TRAVELManager manager = new TASM_TRAVELManager();
            List<DirectionCanderModel> list = new List<DirectionCanderModel>();
            manager.ListByMonth(userid, time, ref list);
            
            return SuccessResultList(list);

        }


        public IActionResult MonthView() {

            return View();
        }

        public IActionResult Canlder()
        {

            return View();
        }


        public IActionResult WrokReport(int traid) {

         

            DataAccess.TASM_TRAVELManager mannager = new TASM_TRAVELManager();
            var model = mannager.SelectByTraid(traid);

            return SuccessResult(model);

        }


        [HttpGet]
        public IActionResult ExportExcelUnion(string date,string keyword = "")
        {

            TASM_TRAVELManager manager = new TASM_TRAVELManager();

            DateTime time = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                time = DateTime.Parse(date);
            }

          
            List<DirectionModel> list = new List<DirectionModel>();

            manager.ListBaseUser(time, keyword,  ref list);


            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("动向信息表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("工号");
            row1.CreateCell(1).SetCellValue("姓名");

            row1.CreateCell(2).SetCellValue("部门");
            row1.CreateCell(3).SetCellValue("职务");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("当月出差天数");


            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].WORK_ID);
                rowTemp.CreateCell(1).SetCellValue(list[i].USER_NAME);

                rowTemp.CreateCell(2).SetCellValue(list[i].DEPARTMENT);
                rowTemp.CreateCell(3).SetCellValue(list[i].DTNAME);
                rowTemp.CreateCell(4).SetCellValue(list[i].MOBILE);
                rowTemp.CreateCell(5).SetCellValue(list[i].MOUNTHCOUNT);
  
            }

            var fileName = "动向信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);

        }


        [HttpGet]
        public IActionResult ExportExcelAll(string date, string keyword = "")
        {

            DateTime? time = null;
            if (!string.IsNullOrEmpty(date))
            {
                time = DateTime.Parse(date);
            }


            TASM_TRAVELManager manager = new TASM_TRAVELManager();


            List<DirectionDetailModel> list = new List<DirectionDetailModel>();

            manager.ListAllByDate(time, keyword, ref list);


            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("动向信息表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("工号");
            row1.CreateCell(1).SetCellValue("姓名");
            row1.CreateCell(2).SetCellValue("部门");


            row1.CreateCell(3).SetCellValue("报告类型");
            row1.CreateCell(4).SetCellValue("时间");
            row1.CreateCell(5).SetCellValue("项目名称");

            row1.CreateCell(6).SetCellValue("客户名称");
            row1.CreateCell(7).SetCellValue("经度");
            row1.CreateCell(8).SetCellValue("纬度");

       
            row1.CreateCell(9).SetCellValue("工单名称");
            row1.CreateCell(10).SetCellValue("地址");

            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].workid);
                rowTemp.CreateCell(1).SetCellValue(list[i].user_name);
                rowTemp.CreateCell(2).SetCellValue(list[i].department);

                rowTemp.CreateCell(3).SetCellValue(list[i].type==0?"早报":"晚报");
                rowTemp.CreateCell(4).SetCellValue(list[i].createtime);
                rowTemp.CreateCell(5).SetCellValue(list[i].project_name);

                rowTemp.CreateCell(6).SetCellValue(list[i].customer_name);
                rowTemp.CreateCell(7).SetCellValue(list[i].longitude.ToString());
                rowTemp.CreateCell(8).SetCellValue(list[i].latitude.ToString());

                rowTemp.CreateCell(9).SetCellValue(list[i].support_title);
                rowTemp.CreateCell(10).SetCellValue(list[i].address);
            }

            var fileName = "动向信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);

        }
    }
}