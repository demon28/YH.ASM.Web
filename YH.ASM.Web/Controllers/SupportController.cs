using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Facade;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    /// <summary>
    /// 工单
    /// </summary>
    public class SupportController : ControllerBase.ComControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ProjectController> logger;

        [Right(PowerName = "工单列表")]
        public IActionResult Index()
        {

            return View();
        }


        [Right(Ignore = true)]
        public IActionResult Attachment()
        {
            return View();
        }
        [Right(Ignore = true)]
        public IActionResult TimeLine()
        {
            return View();
        }

        [Right(Ignore = true)]
        public IActionResult Disposer()
        {
            return View();
        }

        [Right(Ignore = true)]
        public IActionResult PmcOrder()
        {
            return View();
        }


        [Right(Ignore = true)]
        public IActionResult PrincipalCheck()
        {
            return View();
        }

        [Right(Ignore = true)]
        public IActionResult SiteCheck()
        {
            return View();
        }

        [Right(Ignore = true)]

        public IActionResult FillProject()
        {
            return View();
        }

        [Right(Ignore = true)]

        public IActionResult FillUser()
        {
            return View();
        }

        [Right(Ignore = true)]
        public IActionResult Create()
        {
            return View();
        }



        public SupportController(IWebHostEnvironment hostingEnvironment, ILogger<ProjectController> _logger)
        {
            _hostingEnvironment = hostingEnvironment;
            logger = _logger;
        }


        [Right(Ignore = true)]
        public IActionResult AddAndUpdate()
        {
            return View();
        }


        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keywords, int pageIndex, int pageSize, int watchType = 0, string orderby = "SID")
        {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<SupportListModel> list = manager.ListByWhere(keywords, ref p, (SupprotWatchType)watchType, SupprotWatchState.全部, this.UserInfo.USER_ID, orderby);

            return SuccessResultList(list, p);

        }

        [Right(PowerName = "删除")]
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

        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(Entites.CodeGenerator.TASM_SUPPORT model)
        {

            if (model.CREATOR != this.User_Id && model.CONDUCTOR != this.User_Id)
            {
                return FailMessage("抱歉您不是项目相关人员,无法修改项目状态");
            }



            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            manager.Db.BeginTran();
            try
            {


                if (!manager.CurrentDb.Update(model))
                {
                    manager.Db.RollbackTran();
                    return FailMessage();

                }

                DataAccess.TASM_SUPPORT_PERSONAL_Da da = new TASM_SUPPORT_PERSONAL_Da();
                TASM_SUPPORT_PERSONAL personal = da.CurrentDb.AsQueryable().Where(s => s.SID == model.SID && s.STATUS == (int)Entites.SupprotWatchState.待办).First();
                personal.DID = model.CONDUCTOR;

                if (!da.CurrentDb.Update(personal))
                {
                    manager.Db.RollbackTran();
                    return FailMessage();
                }
                manager.Db.CommitTran();
                return SuccessMessage("修改成功");
            }
            catch (Exception e)
            {
                manager.Db.RollbackTran();
                return FailMessage(e.ToString());

            }
        }

        [Right(PowerName = "添加")]
        public IActionResult Add(Entites.CodeGenerator.TASM_SUPPORT model)
        {

            model.CREATETIME = DateTime.Now;
            model.ISDEL = 0;
            model.STATUS = 0;

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            if (!manager.CurrentDb.Insert(model))
            {
                return FailMessage();
            }
            return SuccessMessage("添加成功");

        }

        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetUpdateInfo(int id)
        {
            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SupportListModel model = manager.SelectById(id);

            return SuccessResult(model);

        }

        [Right(Ignore = true)]
        [HttpGet]
        public IActionResult ExportExcel(string keyword = "")
        {
            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();


            List<TASM_SUPPORT> list = new List<TASM_SUPPORT>();
            list = manager.ListByWhere(keyword);

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


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult CreatorInfo()
        {
            return SuccessResult(this.UserInfo);
        }

        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult LisAttachmentt(int id)
        {

            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            List<TASM_ATTACHMENT> list = new List<TASM_ATTACHMENT>();

            manager.ListByPid(id, 1, ref list);

            return SuccessResultList(list);


        }

        [Right(Ignore = true)]
        [HttpPost]
        public ActionResult UploadAttachment()
        {
            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            try
            {

                this.logger.LogInformation("LogInformation:开始上传文件");

                #region

                var path = "/FileUpload/Support/" + UserInfo.USER_ID + "/";
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
                path = "/FileUpload/Support/" + UserInfo.USER_ID + "/" + newFileName;



                #endregion

                TASM_ATTACHMENT model = new TASM_ATTACHMENT();

                model.FILENAME = newFileName;
                model.URL = path;
                model.TYPE = 1;



                manager.Db.BeginTran();

                var newid = manager.Db.Insertable(model).ExecuteReturnIdentity();

                manager.Db.CommitTran();


                var json = new { ID = newid, FILENAME = newFileName, URL = path };

                return SuccessResult(json);

            }
            catch (Exception e)
            {
                manager.Db.RollbackTran();
                logger.LogInformation("异常：" + e);
                return FailMessage(e.ToString());
            }

        }

        [Right(Ignore = true)]
        [HttpPost]
        public ActionResult UploadOther(int sid)
        {
            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            try
            {

                this.logger.LogInformation("LogInformation:开始上传文件");

                #region

                var path = "/FileUpload/Support/" + UserInfo.USER_ID + "/";
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
                path = "/FileUpload/Support/" + UserInfo.USER_ID + "/" + newFileName;



                #endregion

                TASM_ATTACHMENT model = new TASM_ATTACHMENT();

                model.FILENAME = newFileName;
                model.URL = path;
                model.TYPE = 1;
                model.PID = sid;


                manager.Db.BeginTran();

                var newid = manager.Db.Insertable(model).ExecuteReturnIdentity();

                manager.Db.CommitTran();



                var json = new { ID = newid, FILENAME = newFileName, URL = path };

                return SuccessResult(json);

            }
            catch (Exception e)
            {
                manager.Db.RollbackTran();
                logger.LogInformation("异常：" + e);
                return FailMessage(e.ToString());
            }

        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult CreateSupport(SupportCreateModel model)
        {
            Facade.SupportFacade support = new Facade.SupportFacade();
            if (!support.Create(model))
            {
                return FailMessage(support.Msg);
            }
            return SuccessMessage("创建工单成功！");
        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddDisposer(TASM_SUPPORT_DISPOSER model, int supportStatus, int nextUser,int personalId)
        {
            //TODO:web端暂不处理，目前灭有时间
            AddDisposerModel addDisposer = new AddDisposerModel();

            DisposerFacade facade = new DisposerFacade();
            if (!facade.Create(addDisposer))
            {
                return FailMessage(facade.Msg);
            }
            return SuccessMessage("处理成功！");
        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddPmcOrder(TASM_SUPPORT_PMC model, int supportStatus, int nextUser)
        {

            PmcOrderFacade facade = new PmcOrderFacade();

            if (facade.Create(model, supportStatus, nextUser))
            {
                return FailMessage("处理失败！");
            }
            return SuccessMessage("处理成功！");
        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddSiteCheck(TASM_SUPPORT_SITE model, int supportStatus, int nextUser)
        {

            SiteCheckFacade facade = new SiteCheckFacade();

            if (!facade.Create(model, supportStatus, nextUser))
            {
                return FailMessage("处理失败！");
            }

            return SuccessMessage("处理成功！");

        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddPrincipalCheck(TASM_SUPPORT_PRINCIPAL model, int supportStatus, int nextUser)
        {

            PrincipalFacade facade = new PrincipalFacade();

            if (!facade.Create(model, supportStatus, nextUser))
            {
                return FailMessage("处理失败！");
            }

            return SuccessMessage("处理成功！");



        }








        #region 各个处理表信息
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetHisList(int id)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manger = new TASM_SUPPORT_HIS_Da();

            List<SupportHisModel> list = manger.List(id);

            return SuccessResultList(list);
        }

        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetSupportInfo(int id)
        {
            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            TASM_SUPPORT model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);

        }
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetDisposerInfo(int id)
        {
            DataAccess.TASM_SUPPORT_DISPOSER_Da manager = new DataAccess.TASM_SUPPORT_DISPOSER_Da();
            TASM_SUPPORT_DISPOSER model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);

        }
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetPmcInfo(int id)
        {
            DataAccess.TASM_SUPPORT_PMC_Da manager = new DataAccess.TASM_SUPPORT_PMC_Da();
            TASM_SUPPORT_PMC model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);

        }
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetSiteInfo(int id)
        {
            DataAccess.TASM_SUPPORT_SITE_Da manager = new DataAccess.TASM_SUPPORT_SITE_Da();
            TASM_SUPPORT_SITE model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);
        }
        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetPrincipalInfo(int id)
        {
            DataAccess.TASM_SUPPORT_PRINCIPAL_Da manager = new DataAccess.TASM_SUPPORT_PRINCIPAL_Da();
            TASM_SUPPORT_PRINCIPAL model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);
        }

        #endregion
    }
}