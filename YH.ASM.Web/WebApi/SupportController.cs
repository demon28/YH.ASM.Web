using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using SQLitePCL;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Facade;
using YH.ASM.Web.Attribute;
using YH.ASM.Web.ControllerBase;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    public class SupportController : ApiControllerBase
    {
        private static ILogger<SupportController> logger;


        public SupportController(ILogger<SupportController> _logger) {

            logger = _logger;
        }

        [WebApi]
        [HttpPost]
        public IActionResult Create(SupportCreateModel model)
        {

            logger.LogInformation("开始创建工单");

            Facade.SupportFacade support = new Facade.SupportFacade();
            if (!support.Create(model))
            {
                return FailMessage(support.Msg);
            }
            return SuccessMessage("创建工单成功！");

        }


        [WebApi]
        [HttpPost]
        public IActionResult List(ListSupportInputModel model) {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            List<SupportListModel> list = manager.ListByWhere(model.keywords, ref p, model.WatchType, model.WatchState, model.Uuid);

            return SuccessResultList(list, p);

        }

        [WebApi]
        [HttpPost]
        public IActionResult ListPersonal(ListSupportInputModel model)
        {

            TASM_SUPPORT_PERSONAL_Da da = new TASM_SUPPORT_PERSONAL_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            SupprotWatchState state = model.WatchState;
            SupprotWatchType type = model.WatchType;

            List<PersonalSupportListModel> list = da.ListByWhere(string.Empty, ref p, type, state, model.Uuid);

            return SuccessResultList(list, p);

        }



        [WebApi]
        [HttpPost]
        public IActionResult ListSupport(ApiListModelBase model)
        {

            TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;
            

            List<TASM_SUPPORT> list = da.ListByWhere(model.keywords, ref  p);

            return SuccessResultList(list, p);

        }



        [WebApi]
        [HttpPost]
        public IActionResult LisAttachmentt(ListAttachmentInputModel model) {

            TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();

            List<TASM_ATTACHMENT> list = new List<TASM_ATTACHMENT>();

            manager.ListByPid(model.sid, 1, ref list);

            return SuccessResultList(list);

        }


        public IActionResult SelectCount() {

            TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            return SuccessResult(da.SelectCount());
        
        }


        #region 工单处理流程
        [WebApi]
        [HttpPost]
        public IActionResult UpdatePersernalStatus(UpdatePersnalStatusModel model)
        {

            TASM_SUPPORT_PERSONAL_Da da = new TASM_SUPPORT_PERSONAL_Da();

            TASM_SUPPORT_PERSONAL persnalmodel = da.CurrentDb.GetById(model.id);

            persnalmodel.STATUS = model.status;

            if (!da.CurrentDb.Update(persnalmodel))
            {
                return FailMessage("请求失败！");
            }
            return SuccessMessage("处理成功");


        }

        [WebApi]
        [HttpPost]
        public IActionResult AddDisposer(AddDisposerModel model)
        {
            DisposerFacade facade = new DisposerFacade();
            if (!facade.Create(model))
            {
                return FailMessage(facade.Msg);
            }
            return SuccessMessage("处理成功！");
        }


        [WebApi]
        [HttpPost]
        public IActionResult AddPmcOrder(AddPmcCheckModel model)
        {

            PmcOrderFacade facade = new PmcOrderFacade();

            if (facade.Create(model))
            {
                return FailMessage("处理失败！");
            }
            return SuccessMessage("处理成功！");
        }


        [WebApi]
        [HttpPost]
        public IActionResult AddSiteCheck(AddSiteCheckModel model)
        {

            SiteCheckFacade facade = new SiteCheckFacade();

            if (!facade.Create(model))
            {
                return FailMessage("处理失败！");
            }
            return SuccessMessage("处理成功！");

        }


        [WebApi]
        [HttpPost]
        public IActionResult AddPrincipalCheck(AddPrincipalCheckModel model)
        {

            PrincipalFacade facade = new PrincipalFacade();

            if (!facade.Create(model))
            {
                return FailMessage("处理失败！");
            }

            return SuccessMessage("处理成功！");



        }

        #endregion


        #region  工作流
        [WebApi]
        [HttpGet]
        public IActionResult ListByHistory(int sid)
        {

            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            List<TASM_SUPPORT_HIS> list = manager.ListBySid(sid);
            return SuccessResultList(list);
        }


 


        [WebApi]
        [HttpGet]
        public IActionResult GetSupportInfo(int sid,int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisSupportModel model = manager.SelectHisSupport(sid, tid);

            return SuccessResult(model);

        }


        [WebApi]
        [HttpGet]
        public IActionResult GetDisposerInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisDisposerModel model = manager.SelectHisDisposer(sid, tid);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }

        [WebApi]
        [HttpGet]
        public IActionResult GetPmcInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPmcModel model = manager.SelectHisPmc(sid, tid);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }

        [WebApi]
        [HttpGet]
        public IActionResult GetSiteInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisSiteModel model = manager.SelectHisSite(sid, tid);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }


        [WebApi]
        [HttpGet]
        public IActionResult GetPrincipalInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPrincipalModel model = manager.SelectHisPrincipal(sid, tid);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }


        #endregion



    }
}
