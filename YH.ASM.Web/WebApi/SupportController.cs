using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
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
        
        [WebApi]
        [HttpPost]
        public IActionResult Create(SupportCreateModel model)
        {
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

            SupprotWatchState state = (SupprotWatchState)model.WatchState;
            SupprotWatchType type = (SupprotWatchType)model.WatchType;

            List<SupportListModel> list = manager.ListByWhere(string.Empty, ref p, type, state, model.Uuid);

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

            SupprotWatchState state = (SupprotWatchState)model.WatchState;
            SupprotWatchType type = (SupprotWatchType)model.WatchType;

            List<PersonalSupportListModel> list = da.ListByWhere(string.Empty, ref p, type, state, model.Uuid);

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
        public IActionResult AddPmcOrder(TASM_SUPPORT_PMC model, int supportStatus, int nextUser)
        {

            PmcOrderFacade facade = new PmcOrderFacade();

            if (facade.Create(model, supportStatus, nextUser))
            {
                return FailMessage("处理失败！");
            }
            return SuccessMessage("处理成功！");
        }


        [WebApi]
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


        [WebApi]
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




    }
}
