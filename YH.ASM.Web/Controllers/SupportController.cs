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
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
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

        [Right(PowerName ="工单列表")]
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

        [Right(Ignore = true)]

        public IActionResult Details()
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
        public IActionResult List(string keywords, int pageIndex, int pageSize, int watchType=0, string orderby="SID")
        {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<SupportListModel> list=manager.ListByWhere(keywords, ref p, watchType, this.UserInfo.USER_ID, orderby);

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

            if (model.CREATOR!=this.User_Id && model.CONDUCTOR != this.User_Id)
            {
                return FailMessage("抱歉您不是项目相关人员,无法修改项目状态");
            }


            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            if (!manager.CurrentDb.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

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

        [Right(Ignore =true)]
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
            DataAccess.TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            da.Db.BeginTran();
            try
            {
                TASM_SUPPORT supportModel = new TASM_SUPPORT()
                {
                    CREATOR = model.CreatorId,
                    CONDUCTOR = model.ConductorId,
                    CONTENT = model.Content,
                    CREATETIME = DateTime.Now,
                    FINDATE = model.FindDate,
                    ISDEL = 0,
                    MEMBERID = 0,
                    PRIORITY = model.Priority,
                    PROJECT = model.ProjectId,
                    SEVERITY = model.Severity,
                    STATUS = 0,
                    TITLE = model.Title,
                    TYPE = model.Type
                };

               int newid= da.CurrentDb.InsertReturnIdentity(supportModel);



                //当前处理人员发生修改，新增一条 修改记录 history

                DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
                hisModel.CREATETIME = DateTime.Now;
                hisModel.PRE_USER = supportModel.CREATOR;
                hisModel.NEXT_USER = supportModel.CONDUCTOR;
                hisModel.SID = newid;
                hisModel.REMARKS = "工单创建，等待技术处理";

                hisModel.PRE_STATUS = supportModel.STATUS;
                hisModel.NEXT_STATUS = supportModel.STATUS; //初始状态
                hisModel.TYPE = 0;   //tasm_disposer表
                hisModel.TID = newid;


                his_manager.CurrentDb.Insert(hisModel);



                if (model.Filelist!=null && model.Filelist.Count>0)
                {
                    foreach (var item in model.Filelist)
                    {
                        TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();
                        TASM_ATTACHMENT attModel = manager.CurrentDb.GetById(item.ID);
                        attModel.TYPE = 1;
                        attModel.PID = newid;

                        manager.CurrentDb.Update(attModel);
                    }
                }
            


                da.Db.CommitTran();

                return SuccessMessage("创建成功");
            }
            catch (Exception e)
            {
                logger.LogWarning(e.ToString());
                da.Db.RollbackTran();

                return FailMessage("创建工单失败！");
            }
            
            

        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddDisposer(TASM_SUPPORT_DISPOSER model,int supportStatus,int nextUser)
        {

            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;

            DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
            DataAccess.TASM_SUPPORT_DISPOSER_Da disposer_manager = new DataAccess.TASM_SUPPORT_DISPOSER_Da();
        
            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();
            try
            {
                disposer_manager.Db.BeginTran();
          

                //1，添加 工单处理表数据，并获得disposerId
                var disposerId = disposer_manager.Db.Insertable(model).ExecuteReturnIdentity();

             

                //2,修改工单表的memberid，memberid为处理表的主键id 
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息



                //3,当前处理人员发生修改，新增一条 修改记录 history
                TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
                hisModel.CREATETIME = DateTime.Now;
                hisModel.PRE_USER = supportModel.CONDUCTOR;
                hisModel.NEXT_USER = nextUser;
               
                hisModel.SID = model.SID;
                hisModel.REMARKS = model.STATUS==1?"技术已处理，等待PMC处理":"技术已处理，等待现场处理";
                hisModel.PRE_STATUS = supportModel.STATUS;
                
                hisModel.NEXT_STATUS = supportStatus;
                hisModel.TYPE = 1;   //tasm_disposer表
                hisModel.TID = disposerId;

                his_manager.CurrentDb.Insert(hisModel);


                supportModel.MEMBERID = disposerId;    //将处理表的 设置给 工单表   
                supportModel.STATUS = supportStatus;  //修改工单状态
                supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段

                support_manager.CurrentDb.Update(supportModel);  //修改工单表


             

                disposer_manager.Db.CommitTran();

                return SuccessMessage("处理成功！");
            }
            catch (Exception e)
            {
                disposer_manager.Db.RollbackTran();
                return FailMessage("处理失败！");
            }

         


        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddPmcOrder(TASM_SUPPORT_PMC model, int supportStatus, int nextUser)
        {
            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;

            DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
            DataAccess.TASM_SUPPORT_PMC_Da disposer_manager = new DataAccess.TASM_SUPPORT_PMC_Da();

            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();

            try
            {
                disposer_manager.Db.BeginTran();


                //1，添加 PMC处理表数据

               int newid= disposer_manager.Db.Insertable(model).ExecuteReturnIdentity(); ;



                //2,修改工单表的状态
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息



                //3,当前处理人员发生修改，新增一条 修改记录 history
                TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();

                hisModel.CREATETIME = DateTime.Now;
                hisModel.PRE_USER = supportModel.CONDUCTOR;
                hisModel.NEXT_USER = nextUser;

                hisModel.SID = model.SID;
                hisModel.REMARKS = "PMC已处理,等待现场处理";
                hisModel.PRE_STATUS = supportModel.STATUS;
            
                hisModel.NEXT_STATUS = supportStatus;
                hisModel.TYPE = 2;   //tasm_disposer表
                hisModel.TID = newid;

                his_manager.CurrentDb.Insert(hisModel);

        
                supportModel.STATUS = supportStatus;  //修改工单状态
                supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段

                support_manager.CurrentDb.Update(supportModel);  //修改工单表

                disposer_manager.Db.CommitTran();

                return SuccessMessage("处理成功！");
            }
            catch (Exception e)
            {
                disposer_manager.Db.RollbackTran();
                return FailMessage("处理失败！");
            }

        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddSiteCheck(TASM_SUPPORT_SITE model, int supportStatus, int nextUser)
        {
            model.CREATETIME = DateTime.Now;

            DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
            DataAccess.TASM_SUPPORT_SITE_Da disposer_manager = new DataAccess.TASM_SUPPORT_SITE_Da();

            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();


            try
            {
                disposer_manager.Db.BeginTran();



                //1，添加 PMC处理表数据，

                int newid = disposer_manager.Db.Insertable(model).ExecuteReturnIdentity();



                //2,修改工单表的状态
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息


                string remark ="现场已处理，等待审核";

                //3,当前处理人员发生修改，新增一条 修改记录 history
                TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
                hisModel.CREATETIME = DateTime.Now;
                hisModel.PRE_USER = supportModel.CONDUCTOR;
                hisModel.NEXT_USER = nextUser;
               
                hisModel.SID = model.SID;
                hisModel.REMARKS = remark;
                hisModel.PRE_STATUS = supportModel.STATUS;
                
                hisModel.NEXT_STATUS = supportStatus;
                hisModel.TYPE = 3;   //tasm_disposer表
                hisModel.TID = newid;

                his_manager.CurrentDb.Insert(hisModel);




                supportModel.STATUS = supportStatus;  //修改  新状态工单状态
                supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段

                support_manager.CurrentDb.Update(supportModel);  //修改工单表

                disposer_manager.Db.CommitTran();

                return SuccessMessage("处理成功！");
            }
            catch (Exception e)
            {
                disposer_manager.Db.RollbackTran();
                return FailMessage("处理失败！");
            }

        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult AddPrincipalCheck(TASM_SUPPORT_PRINCIPAL model, int supportStatus, int nextUser)
        {
            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;

            DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
            DataAccess.TASM_SUPPORT_PRINCIPAL_Da disposer_manager = new DataAccess.TASM_SUPPORT_PRINCIPAL_Da();

            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();

            try
            {
                disposer_manager.Db.BeginTran();


                //1，添加 PMC处理表数据，并获得新id
                int newid= disposer_manager.Db.Insertable(model).ExecuteReturnIdentity();


                //2,修改工单表的状态
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息


                string remarks = supportStatus == 5 ? "未完成" : "已完成";

                //3,当前处理人员发生修改，新增一条 修改记录 history
                TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
                hisModel.CREATETIME = DateTime.Now;
                hisModel.PRE_USER = supportModel.CONDUCTOR;
                hisModel.NEXT_USER = nextUser;
               
                hisModel.SID = model.SID;
                hisModel.REMARKS = "负责人已审核:"+remarks;
                hisModel.PRE_STATUS = supportModel.STATUS;
                
                hisModel.NEXT_STATUS = supportStatus;
                hisModel.TYPE = 4;   //tasm_disposer表
                hisModel.TID = newid;

                his_manager.CurrentDb.Insert(hisModel);


                supportModel.STATUS = supportStatus;  //修改工单状态
                supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段

                support_manager.CurrentDb.Update(supportModel);  //修改工单表

                disposer_manager.Db.CommitTran();

                return SuccessMessage("处理成功！");
            }
            catch (Exception e)
            {
                disposer_manager.Db.RollbackTran();
                return FailMessage("处理失败！");
            }

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