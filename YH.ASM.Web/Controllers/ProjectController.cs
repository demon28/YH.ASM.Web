using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.Controllers
{
    //项目履历
    public class ProjectController : ControllerBase.ControllerBase
    {
      

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddAndUpdate()
        {
            return View();
        }
        public IActionResult Attachment()
        {
            return View();
        }

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IWebHostEnvironment hostingEnvironment, ILogger<ProjectController> _logger)
        {
            _hostingEnvironment = hostingEnvironment;
             logger =_logger;



        }


        [HttpPost]
        public IActionResult List(string keywords,int pageIndex, int pageSize)
        {
            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();

            SqlSugar.PageModel p = new SqlSugar.PageModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            List<TASM_PROJECT> list = new List<TASM_PROJECT>();
            manager.ListByWhere(keywords, ref p, ref list);

            return SuccessResultList(list, p);
        }

        public IActionResult Add(Entites.CodeGenerator.TASM_PROJECT model)
        {

            model.CREATETIME = DateTime.Now;
            model.ISDEL = 0;
            model.STATUS = 0;

            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();
            if (!manager.Insert(model))
            {
                return FailMessage();
            }
            return SuccessMessage("添加成功");

        }


      
        public IActionResult Update(Entites.CodeGenerator.TASM_PROJECT model)
        {

            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();
            if (!manager.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

        }

        [HttpPost]
        public IActionResult Delete(int pid)
        {

            TASM_PROJECTManager dbContext = new TASM_PROJECTManager();
            if (!dbContext.CurrentDb.Delete(S => S.PID == pid))
            {
                return FailMessage();
            }
            return SuccessMessage();


        }

        [HttpGet]
        public IActionResult ExportExcel( string keyword = "")
        {
            DataAccess.TASM_PROJECTManager manager = new DataAccess.TASM_PROJECTManager();


            List<TASM_PROJECT> list = new List<TASM_PROJECT>();
            manager.ListByWhere(keyword,  ref list);

            HSSFWorkbook excelBook = new HSSFWorkbook(); //创建工作簿Excel
            ISheet sheet1 = excelBook.CreateSheet("项目履历表");

            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("项目名称");
            row1.CreateCell(1).SetCellValue("客户名称");

            row1.CreateCell(2).SetCellValue("出货时间");
            row1.CreateCell(3).SetCellValue("设备名称");
            row1.CreateCell(4).SetCellValue("数量");
            row1.CreateCell(5).SetCellValue("安装负责人");




            row1.CreateCell(6).SetCellValue("一级负责人");
            row1.CreateCell(7).SetCellValue("二级负责人");
            row1.CreateCell(8).SetCellValue("三级负责人");

            row1.CreateCell(9).SetCellValue("安装时效-开始时间");
            row1.CreateCell(10).SetCellValue("安装时效-结束时间");
            row1.CreateCell(11).SetCellValue("安装时效-安装天数");
            row1.CreateCell(12).SetCellValue("安装进度");

            row1.CreateCell(13).SetCellValue("调试时效-开始时间");
            row1.CreateCell(14).SetCellValue("调试时效-结束时间");
            row1.CreateCell(15).SetCellValue("调试天数");
            row1.CreateCell(16).SetCellValue("调试进度");



            row1.CreateCell(17).SetCellValue("验收-开始时间");
            row1.CreateCell(18).SetCellValue("验收-结束时间");
            row1.CreateCell(19).SetCellValue("验收倒计时(天)");
            row1.CreateCell(20).SetCellValue("创建时间");



            for (int i = 0; i < list.Count(); i++)
            {

                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                rowTemp.CreateCell(0).SetCellValue(list[i].NAME);
                rowTemp.CreateCell(1).SetCellValue(list[i].CUSTOMER_NAME);

                rowTemp.CreateCell(2).SetCellValue(list[i].PRODUCTDATE.ToString());
                rowTemp.CreateCell(3).SetCellValue(list[i].MACHINE);
                rowTemp.CreateCell(4).SetCellValue(list[i].COUNT.ToString());
                rowTemp.CreateCell(5).SetCellValue(list[i].INSTALL_NAME);


                rowTemp.CreateCell(6).SetCellValue(list[i].INSTALL_LV1);
                rowTemp.CreateCell(7).SetCellValue(list[i].INSTALL_LV2);

                rowTemp.CreateCell(8).SetCellValue(list[i].INSTALL_LV3);
                rowTemp.CreateCell(9).SetCellValue(list[i].INSTALL_STARDATE.ToString());
                rowTemp.CreateCell(10).SetCellValue(list[i].INSTALL_ENDDATE.ToString());
                rowTemp.CreateCell(11).SetCellValue(list[i].INSTALL_DAYS.ToString());

               
                rowTemp.CreateCell(12).SetCellValue((Enum.GetName(typeof(Entites.EnumTypes.ProjectStatus), list[i].INSTALL_STATUS)));


                rowTemp.CreateCell(13).SetCellValue(list[i].DEBUG_STARDATE.ToString());

                rowTemp.CreateCell(14).SetCellValue(list[i].DEBUG_ENDDATE.ToString());
                rowTemp.CreateCell(15).SetCellValue(list[i].DEBUG_DAYS.ToString());

                
                rowTemp.CreateCell(16).SetCellValue(Enum.GetName(typeof(Entites.EnumTypes.ProjectStatus), list[i].DEBUG_STATUS));
                
                rowTemp.CreateCell(17).SetCellValue(list[i].CHECK_STARDATE.ToString());


                rowTemp.CreateCell(18).SetCellValue(list[i].CHECK_ENDDATE.ToString());
                rowTemp.CreateCell(19).SetCellValue(list[i].COUNT_DOWN.ToString());
                rowTemp.CreateCell(20).SetCellValue(list[i].CREATETIME.ToString());

            }

            var fileName = "项目履历表" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";//文件名

            //将Excel表格转化为流，输出

            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);



        }

        [HttpPost]
        public IActionResult GetUpdateInfo(int id)
        {
            TASM_PROJECTManager manager = new TASM_PROJECTManager();
            TASM_PROJECT model = manager.GetById(id);

            return SuccessResult(model);

        }

        [HttpPost]
        public IActionResult LisAttachmentt(int id)
        {

            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            List<TASM_ATTACHMENT> list = new List<TASM_ATTACHMENT>();

            manager.ListByPid(id, ref list);

            return SuccessResultList(list);


        }

        [HttpPost]
        public IActionResult DeleteAttachment(int aid) {

            DataAccess.TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            if (!manager.CurrentDb.DeleteById(aid))
            {
                return FailMessage();
            }
            return SuccessMessage();

        }

        [HttpPost]
        public ActionResult UploadAttachment(int pid)
        {
            try
            {

                this.logger.LogInformation("LogInformation:开始上传文件");

                #region

                var path = "/FileUpload/Project/" + UserInfo.USER_ID + "/";
                var files = Request.Form.Files;

                if (files.Count<=0)
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
                    path= "/FileUpload/Project/" + UserInfo.USER_ID + "/" + newFileName;



                #endregion

                TASM_ATTACHMENT model = new TASM_ATTACHMENT();
                model.PID = pid;
                model.FILENAME = newFileName;
                model.URL = path;


                if (!AddAttachment( model))
                {
                    logger.LogInformation("上传文件 数据库添加失败");
                    return FailMessage("上传失败！");
                }
                logger.LogDebug("上传文件成功");
                return SuccessMessage(path);

            }
            catch (Exception e)
            {
                logger.LogInformation("异常："+e);
                return FailMessage(e.ToString());
            }

        }
   
    
        public bool AddAttachment(TASM_ATTACHMENT model) {


            DataAccess.TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            return manager.Insert(model);
        
        }



    }

  
}