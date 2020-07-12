using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class SupportTimeLineController : ControllerBase.ComControllerBase
    {

        [Right(PowerName = "工单历史")]
        public IActionResult Index()
        {
            return View();
        }

        [Right(PowerName = "工单详情")]
        public IActionResult TimeLine()
        {
            return View();

        }


        [Right(PowerName = "工单流程")]

        public IActionResult WorkFolw()
        {
            return View();

        }


        [Right(Ignore =true)]
        [HttpPost]
        public IActionResult ListByHis(int sid)
        {

            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            List<TASM_SUPPORT_HIS> list = manager.Db.Queryable<TASM_SUPPORT_HIS>().Where(s => s.SID == sid).OrderBy(s => s.CREATETIME,SqlSugar.OrderByType.Asc).ToList();
            return SuccessResultList(list);
        }


        #region  工作流


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetSupportInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisSupportModel model = manager.SelectHisSupport(sid, tid);

            return SuccessResult(model);

        }



        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetDisposerInfo(int sid,int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisDisposerModel model = manager.SelectHisDisposer(sid, tid);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }


        [Right(Ignore = true)]
        [HttpPost]
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

        [Right(Ignore = true)]
        [HttpPost]
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


        [Right(Ignore = true)]
        [HttpPost]
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
