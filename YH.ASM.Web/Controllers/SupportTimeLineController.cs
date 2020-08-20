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



        #region  修改工单流




        [Right(PowerName = "修改工单内容")]
        public IActionResult SupportEditor() {

            return View();
        
        }
        [HttpPost]
        public IActionResult SupportInfo(int sid)
        {
            TASM_SUPPORT_Da support = new TASM_SUPPORT_Da();
            var model = support.SelectById(sid);
            return SuccessResult(model);
        }

        [HttpPost]
        public IActionResult SupportUpdate(TASM_SUPPORT info)
        {

            TASM_SUPPORT_Da support = new TASM_SUPPORT_Da();

            TASM_SUPPORT_HIS_Da his = new TASM_SUPPORT_HIS_Da();

            var hisModel= his.SelectBySidType(info.SID,0);

            support.Db.BeginTran();

            hisModel.PRE_USER = info.CREATOR;
            hisModel.NEXT_USER = info.CONDUCTOR;

            if (his.Db.Updateable(hisModel).ExecuteCommand()<1)
            {
                support.Db.RollbackTran();
                return FailMessage();
            }


            if (support.Db.Updateable(info).ExecuteCommand()<1)
            {
                support.Db.RollbackTran();
                return FailMessage();
            }

            support.Db.CommitTran();
            return SuccessMessage();

        }



        [Right(PowerName = "修改现场负责人内容")]
        public IActionResult DisposerEditor()
        {
            return View();
        }
   
        public IActionResult DisposerInfo(int sid,int tid) {

          
                DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
                HisDisposerModel model = manager.SelectHisDisposer(sid, tid);

                return SuccessResult(model);

        }

        [HttpPost]
        public IActionResult DisposerUpdate(AddDisposerModel info) {

            TASM_SUPPORT_DISPOSER_Da disposer = new TASM_SUPPORT_DISPOSER_Da();

            TASM_SUPPORT_HIS_Da his = new TASM_SUPPORT_HIS_Da();

            var hisModel = his.SelectBySidType(info.SID, 1);

            disposer.Db.BeginTran();

            hisModel.NEXT_USER = info.NEXTUSER ;

            if (his.Db.Updateable(hisModel).ExecuteCommand() < 1)
            {
                disposer.Db.RollbackTran();
                return FailMessage();
            }


            TASM_SUPPORT_DISPOSER model = new TASM_SUPPORT_DISPOSER() {

                ANALYZE = info.ANALYZE,
                ANALYZEUSER = info.ANALYZEUSER,
                BOM = info.BOM,
                DUTY = info.DUTY,
                ID = info.ID,
                ISORDER = info.ISORDER,
                ORDERMAN = info.ORDERMAN,
                ORDERTIME = info.ORDERTIME,
                RESPONSIBLE = info.RESPONSIBLE,
                SID = info.SID,
                SOLUTION = info.SOLUTION,
                STATUS = info.STATUS,
                CREATETIME = info.CREATETIME,
                 REMARKS=info.REMARKS
            
            };
           

            if (disposer.Db.Updateable(model).ExecuteCommand() < 1)
            {
                disposer.Db.RollbackTran();
                return FailMessage();
            }

            disposer.Db.CommitTran();
        
            return SuccessMessage();



        }




        [Right(PowerName = "修改售后内勤维护内容")]

        public IActionResult PmcEditor() {
            return View();
        }


        [HttpPost]
        public IActionResult PmcInfo(int sid, int tid) {

            TASM_SUPPORT_HIS_Da manager = new TASM_SUPPORT_HIS_Da();
            HisPmcModel model = manager.SelectHisPmc(sid, tid);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }


        #endregion


    }
}
