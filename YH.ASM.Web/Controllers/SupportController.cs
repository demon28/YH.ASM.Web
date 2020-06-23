using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    /// <summary>
    /// 工单
    /// </summary>
    public class SupportController : ControllerBase.ControllerBase
    {


        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ProjectController> logger;

        [AuthRight]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public  SupportController(IWebHostEnvironment hostingEnvironment, ILogger<ProjectController> _logger)
        {
            _hostingEnvironment = hostingEnvironment;
            logger = _logger;
        }


        #region  代码生成器 生成

        public IActionResult AddAndUpdate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(string keywords, int pageIndex, int pageSize)
        {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TASM_SUPPORT> list=manager.ListByWhere(keywords, ref p);

            return SuccessResultList(list, p);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            if (!manager.CurrentDb.Delete(S => S.SID == id))
            {
                return FailMessage();
            }
            return SuccessMessage();


        }
        [HttpPost]
        public IActionResult Update(Entites.CodeGenerator.TASM_SUPPORT model)
        {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            if (!manager.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

        }
        public IActionResult Add(Entites.CodeGenerator.TASM_SUPPORT model)
        {

            model.CREATETIME = DateTime.Now;
            model.ISDEL = 0;
            model.STATUS = 0;

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            if (!manager.Insert(model))
            {
                return FailMessage();
            }
            return SuccessMessage("添加成功");

        }

        [HttpPost]
        public IActionResult GetUpdateInfo(int id)
        {
            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            TASM_SUPPORT model = manager.GetById(id);

            return SuccessResult(model);

        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExportExcel(string keyword = "")
        {
            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();


            List<TASM_SUPPORT> list = new List<TASM_SUPPORT>();
            list= manager.ListByWhere(keyword);

            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("项目履历表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("概要");
            row1.CreateCell(1).SetCellValue("创建者");
            row1.CreateCell(2).SetCellValue("处理人");
            row1.CreateCell(3).SetCellValue("发现时间");
            row1.CreateCell(4).SetCellValue("创建时间");


            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].TITLE);
                rowTemp.CreateCell(1).SetCellValue(list[i].CREATOR);
                rowTemp.CreateCell(2).SetCellValue(list[i].CONDUCTOR);
                rowTemp.CreateCell(3).SetCellValue(list[i].FINDATE);
                rowTemp.CreateCell(4).SetCellValue(list[i].CREATETIME.ToString());

            }

            var fileName = "工单信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);

        }

        #endregion


        [HttpPost]
        public IActionResult CreatorInfo()
        {
            return SuccessResult(this.UserInfo);
        }


        [HttpPost]
        public ActionResult UploadAttachment()
        {
            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            try
            {

                this.logger.LogInformation("LogInformation:开始上传文件");

                #region

                var path = "/FileUpload/Project/" + UserInfo.USER_ID + "/";
                var files = Request.Form.Files;

                if (files.Count <= 0)
                {
                    return FailMessage("请选择要上传的文件");
                }

                var file = files[0];


                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                string fileExt = Path.GetExtension(file.FileName);


                string newFileName = filename.Remove(filename.LastIndexOf('.')) + "(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + fileExt;


                string filepath = path + newFileName;
                string fullpath = _hostingEnvironment.WebRootPath + $@"\{filepath}";

                string dirpath = _hostingEnvironment.WebRootPath + path;



                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }


                using (FileStream fs = System.IO.File.Create(fullpath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                path = "/FileUpload/Project/" + UserInfo.USER_ID + "/" + newFileName;



                #endregion

                TASM_ATTACHMENT model = new TASM_ATTACHMENT();
               
                model.FILENAME = newFileName;
                model.URL = path;
                model.TYPE = 1;



                manager.Db.BeginTran();

                var newid= manager.Db.Insertable(model).ExecuteReturnIdentity();

                manager.Db.CommitTran();



                var json= new { ID = newid, FILENAME = newFileName, URL = path };

                return SuccessResult(json);

            }
            catch (Exception e)
            {
                manager.Db.RollbackTran();
                  logger.LogInformation("异常：" + e);
                return FailMessage(e.ToString());
            }

        }


    }
}